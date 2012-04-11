using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1;

namespace tacocopterbase
{
	class Object : DrawableGameComponent {
		public State2D State { get; set; }
		protected Texture2D sprite;
		protected SpriteBatch spriteBatch { get; set; }
		protected Vector2 offset;
		protected Game thisGame;

		// commented this out because State2D does not
		// have a default constructor and it was an error
		public Object(Game g)
			: base(g) {
			State = new State2D();
			thisGame = g;
		}

		public Object(State2D s, Game g)
			: base(g) {
			State = s;
			thisGame = g;
		}

		// create a SpriteBatch for drawing and load the sprite
		public override void Initialize() {
			spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
			LoadContent();
			base.Initialize();
		}

		// draw the Object at State.Position, factoring in an offset
		public virtual void Draw(SpriteBatch batch, GameTime gametime) {
			
			batch.Draw(sprite, State.Position + offset, null, Color.White, 0,
				new Vector2(0, 0), 0.3f, SpriteEffects.None, 0);
			
		}

		// load the default sprite
		protected override void LoadContent() {
			sprite = thisGame.Content.Load<Texture2D>("black_box");
			offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
			base.LoadContent();
		}

		// updates position of the Object based on velocity
		// and updates velocity of the Object based on acceleration
		public override void Update(GameTime gameTime) {
			float timeInterval = (float)gameTime.ElapsedGameTime.TotalSeconds;
			State.Position += State.Velocity * timeInterval;
			State.Velocity += State.Acceleration * timeInterval;
			base.Update(gameTime);
		}

		// return a new copy of the Object with the same attributes
		public Object Copy() {
			return new Object(State, thisGame);
		}
	}

	class Tacocopter : Object
	{
		private TimeSpan lastFire;
		private int fireRate = 100;
		public List<Taco> tacos = new List<Taco>();
		public int Speed { get; set; }

        private AnimatedTexture SpriteTexture;
        private const float Rotation = 0;
        private const float Scale = 0.5f;
        private const float Depth = 0.5f;

		public Tacocopter(State2D s, Game g)
			: base(g)
		{
			thisGame = g;
			State = s;
			Speed = 5;

            SpriteTexture = new AnimatedTexture(Vector2.Zero,
                Rotation, Scale, Depth);
		}
		

		public void LoadContent()
		{
			//sprite = this.Game.Content.Load<Texture2D>("helicopter1");

			//offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
            SpriteTexture.Load(thisGame.Content, "main_helicopter", 5, 5, new Vector2(3, 2));
		}

		protected void FireTaco()
		{
		   // if (gameTime.TotalGameTime.Subtract(lastFire).TotalMilliseconds >= fireRate)
			{
				Taco taco = null;
				taco = new Taco(thisGame, new State2D(State.Position.X, State.Position.Y, 0, 120, 0, 200, 0));
				
				tacos.Add(taco);
				Game.Components.Add(taco);

				//lastFire = gameTime.TotalGameTime;
			}
		}

		protected void CheckTacos() {/*
			List<Taco> removed = new List<Taco>();

			foreach (Taco taco in tacos) {
				if (taco.Offscreen) {
					Game.Components.Remove(taco);
					removed.Add(taco);
				}
			}
			foreach (Taco taco in removed) {
				tacos.Remove(taco);
			}*/
		}
        public override void Draw(SpriteBatch batch,GameTime gameTime)
        {

            SpriteTexture.DrawFrame(batch, State.Position);
        }


		// Matt, I've rewritten this to be compatible with the gameTime
		// style of Update functions
		public override void Update(GameTime gameTime) {

			KeyboardState k = Keyboard.GetState();
			Vector2 nextPosition = new Vector2(0,0);
			
            if (k.IsKeyDown(Keys.Left) && State.Position.X > 10) {
				nextPosition.X += -Speed;
			}
			if (k.IsKeyDown(Keys.Right) && State.Position.X <640) {
				nextPosition.X += Speed;
			}
			if (k.IsKeyDown(Keys.Up) && State.Position.Y > 10) {
				nextPosition.Y += -Speed;
			}
			if (k.IsKeyDown(Keys.Down) && State.Position.Y < 360) {
				nextPosition.Y += Speed;
			}
			if (k.IsKeyDown(Keys.Space)){
				FireTaco();
			}

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your game logic here.
            SpriteTexture.UpdateFrame(elapsed);
           

			// delete offscreen tacos
			CheckTacos();

			// Tacos are already updated because they're in Components
			/*
			foreach (Taco taco in tacos) {
				taco.Update(gameTime);
			}
			*/

			State.Position += nextPosition;
			base.Update(gameTime);
		}
	}

	class Taco : Object {
		private bool offscreen;
		public bool Offscreen { 
			get { return offscreen;} 
		}

		public Taco(Game g, State2D s)
			: base(g) {
			thisGame = g;
			State = s;
		}

		protected override void LoadContent() {
			sprite = this.Game.Content.Load<Texture2D>("black_box");
			offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
			if (State.Position.Y > 700) {
				offscreen = true;
			}
			base.Update(gameTime);
		}
	}
}
