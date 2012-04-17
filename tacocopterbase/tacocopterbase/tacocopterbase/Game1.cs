using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WindowsGame1;


namespace tacocopterbase
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		State2D genState;
		public Player p1;
        public ScrollingBackground myBackground;
        Texture2D pauseButton;

        private TimeSpan lastFire;
        private int firerate = 500;

        BurritoGenerator<Burrito> enemy;

        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;

        HealthBar myHealthBar;

		// for drawing on screen
		SpriteFont Arial;

		// define game dimensions
		const int windowHeight = 700, windowWidth = 1280;

		/// <summary>
		/// 
		/// </summary>
		public Game1()
		{
            
			// set screen Size
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferHeight = windowHeight;
			graphics.PreferredBackBufferWidth = windowWidth;

			//this.graphics.IsFullScreen = true;

			// show Mouse cursor for debugging
			this.IsMouseVisible = true;

            Init();
		}

        private void Init()
        {
            // create an object with a person sprite moving left
            // create an objectgenerator to test its functionality
            // genState = new State2D(windowWidth - 100, windowHeight - 100, 0, 0, -100, 0, 0);

            // to use an ObjectGenerator, you must show it how to 
            // use the constructor for the type T that you want to generate
            // e.g. (a, b) => new Object(a, b) [shown below]
            /*
            Components.Add(new ObjectGenerator<Object>(
                (a, b) => new Object(a, b),
                genState, 0.2f, this));
            */

            // add a Tacocopter for the player to manipulate
            Components.Add(new Tacocopter(new State2D(400, 200), this));

            // generate some sidewalk
            /*genState = new State2D(0, windowHeight, 0, 0, -100, 0, 0);
			Components.Add(new ObjectGenerator<Sidewalk>(
				(a, b) => new Sidewalk(a, b),
				genState, .1f, this));
				genState, .1f, this));*/

            // generate some generic people
            genState = new State2D(windowWidth, windowHeight - 50, 0, 0, -180, 0, 0);
            Components.Add(new CustomerGenerator<Customer>(
                (a, b) => new Customer(a, b),
                genState, 1, 4, this));

            // test out Burrito missiles
            genState = new State2D(windowWidth + 50, windowHeight /* doesn't matter */, 0, 0, -800, 0, 0);
            enemy = new BurritoGenerator<Burrito>(
                (a, b) => new Burrito(a, b),
                genState, 4f, 140, windowHeight - 220, this);

            Components.Add(enemy);


            //player class to hold score ---- very rudimentary
            p1 = new Player(this);
            Components.Add(p1);

            myHealthBar = new HealthBar();
            myBackground = new ScrollingBackground();
            p1.Score = 10;
        }



        

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// load Arial font for writing on screen
			Arial = Content.Load<SpriteFont>("Arial");

            Texture2D healthBar = Content.Load<Texture2D>("HealthBar2");
            myHealthBar.Load(GraphicsDevice, healthBar);

			// load scrolling background
            Texture2D backgroundTexture = Content.Load<Texture2D>("chic4");
            pauseButton = Content.Load<Texture2D>("pause_button");

            myBackground.Load(GraphicsDevice, backgroundTexture);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{

            KeyboardState k = Keyboard.GetState();

			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

            checkPauseKey(k);

            enemy.Interval = enemy.Interval * 0.99935f;
            
            // If the user hasn't paused, Update normally
            if (!paused)
            {
                // Hit F to toggle fullscreen
                if (k.IsKeyDown(Keys.F))
                    this.graphics.IsFullScreen = !this.graphics.IsFullScreen;
                if (k.IsKeyDown(Keys.R))
                {
                    p1.UnLose();
                    p1.Health = 100;
                    enemy.Interval = 4f;
                    ClearGame();
                    Components.Add(new Tacocopter(new State2D(400, 200), this));

                }
                if (k.IsKeyDown(Keys.Space))
                {
                    if (gameTime.TotalGameTime.Subtract(lastFire).TotalMilliseconds >= firerate)
                    {
                        p1.Score = p1.Score - 1;
                        lastFire = gameTime.TotalGameTime;
                    }
                }
                // scroll the background
                myBackground.Update((float)gameTime.ElapsedGameTime.TotalSeconds * 50);

                // update player's health
                p1.Health = (int)MathHelper.Clamp(p1.Health, 0, 100);

                // Check for collisions and remove and unnecessary objects
                CheckCollisions();
                ClearOffscreenObjects();

                base.Update(gameTime);

                if (p1.Score < 0)
                {
                    p1.Lose(); 
                }
                if (p1.youLose)
                {
                    ClearGame();
                }

            }
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			//  Begin sprite batch ------------------------------
			spriteBatch.Begin();

			// draw the scrolling background first
			myBackground.Draw(spriteBatch);

			Object o;
			foreach (var c in Components)
			{
				o = c as Object;
				if (o != null)
				{
					o.Draw(spriteBatch, gameTime);
				}
			}
			spriteBatch.End();
			// End sprite batch -------------------------------

			// TODO: Add your drawing code here
			base.Draw(gameTime);
		}

        public void ClearGame()
            // Clears the game of all objects
        {
            Object o;
            List<Object> toRemove = new List<Object>();
            foreach (var c in Components)
            {
                o = c as Object;
                if (o != null)
                { toRemove.Add(o); }
            }
            foreach (Object r in toRemove)
                Components.Remove(r);
        }

		/// <summary>
		/// Destroy offscreen objects
		/// </summary>
		private void ClearOffscreenObjects()
		{
			Object o;
            Customer fail;
			var toRemove = new List<Object>();
			foreach (var c in Components)
			{
				o = c as Object;
				if (o != null)
				{
					if (o.State.Position.X < -100 ||
						o.State.Position.X > windowWidth + 200 ||
						o.State.Position.Y < -100 ||
						o.State.Position.Y > windowHeight + 100)
					{
						toRemove.Add(o);
                        fail = o as Customer;
                        if (fail != null)
                        {
                            p1.Health = p1.Health - 10;
                        }

					}
				}
			}
			foreach (Object r in toRemove)
				Components.Remove(r);
		}

		/// <summary>
		/// Detect and handle colliding objects. 
		/// </summary>
		private void CheckCollisions()
		{
			Object o, p;
			Taco ct;
			Customer cc;
			Tacocopter tc;
			Burrito br;
			var toRemove = new List<Object>();
			foreach (var c in Components)
			{
				o = c as Object;
				if (o != null)
				{
					foreach (var d in Components)
					{
						p = d as Object;
						if (d != null)
						{
							ct = o as Taco;
							cc = p as Customer;
							if (ct != null && cc != null)
							{
								if (Object.AreColliding(ct, cc))
								{
									p1.Score = p1.Score + 3;
									toRemove.Add(ct);
									toRemove.Add(cc);
								}
							}

							br = o as Burrito;
							tc = p as Tacocopter;
							if (br != null && tc != null)
							{
								if (Object.AreColliding(br, tc))
								{
									toRemove.Add(tc);
									toRemove.Add(br);
                                    p1.Lose();
								}
							}
						}
					}
				}
			}

			foreach (var obj in toRemove)
			{
				Components.Remove(obj);
			}
		}
        
        private void BeginPause(bool UserInitiated)
        {
            paused = true;
            pausedForGuide = !UserInitiated;
        }

        private void EndPause()
        {
            pausedForGuide = false;
            paused = false;
        }

        private void checkPauseKey(KeyboardState keyboardState)
        {
            bool pauseKeyDownThisFrame = (keyboardState.IsKeyDown(Keys.P));
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!paused)
                    BeginPause(true);
                else
                    EndPause();
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }
       
	}
}