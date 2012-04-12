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
			
		const int windowHeight = 720, windowWidth = 1280;

		/** this is the place we will declare the object classes**/

		public Game1() {
			// set screen Size
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferHeight = windowHeight;
			graphics.PreferredBackBufferWidth = windowWidth;

			// show Mouse cursor for debugging
			this.IsMouseVisible = true;

			// create an object with a person sprite moving left
			// create an objectgenerator to test its functionality
			var testObj = new Object(new State2D(windowWidth-100, windowHeight-100, 0, 0, -10, -10, 0), this);
			//Components.Add(new ObjectGenerator<Object>(new State2D(windowWidth - 100, windowHeight - 100, 0, 0, -10, -10, 0), this));

			//Components.Add(new Customer(new State2D(windowWidth - 100, windowHeight - 100, 0, 0, -10, -10, 0), this));

			// add a Tacocopter for the player to manipulate
			Components.Add(new Tacocopter(new State2D(400, 200, 0, 0, 0, 0, 0), this));
		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			//baseObject.LoadContent();

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here
			ClearOffscreenObjects();
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// is this a kludge?
			spriteBatch.Begin();
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

			// TODO: Add your drawing code here
			base.Draw(gameTime);
		}

		// destroy offscreen objects
		private void ClearOffscreenObjects() {
			Object o;
			var toRemove = new List<Object>();
			foreach (var c in Components) {
				o = c as Object;
				if (o != null) {
					if (o.State.Position.X < -100 ||
						o.State.Position.X > windowWidth + 100 ||
						o.State.Position.Y < -100 ||
						o.State.Position.Y > windowHeight + 100) {
						toRemove.Add(o);
					}
				}
			}
			foreach (Object r in toRemove)
				Components.Remove(r);
		}
	}
}