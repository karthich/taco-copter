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
		protected Vector2 origin;
		protected Game thisGame;
		protected int boundLeft, boundTop;

		public Object(Game g) : base(g) {
			State = new State2D();
			thisGame = g;
		}

		public Object(State2D s, Game g) : base(g) {
			State = s;
			thisGame = g;
		}

		public override void Initialize() {
			spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
			LoadContent();
			base.Initialize();
		}

		// draw the Object at State.Position with origin
		public virtual void Draw(SpriteBatch batch, GameTime gametime) {
			batch.Draw(sprite, State.Position, null, Color.White, 0,
				origin, 0.3f, SpriteEffects.None, 0);
		}

		protected override void LoadContent() {
			//sprite = thisGame.Content.Load<Texture2D>("black_box"); // the default sprite
			origin = new Vector2(sprite.Width, sprite.Height) / 2;
			boundLeft = sprite.Width / 2;
			boundTop = sprite.Height / 2;
			base.LoadContent();
		}

		// updates position of the Object based on velocity
		// and updates velocity of the Object based on acceleration
		public override void Update(GameTime gameTime) {
			float timeInterval = (float)gameTime.ElapsedGameTime.TotalSeconds;
			State.Position += State.Velocity * timeInterval;
			State.Velocity += State.Acceleration * timeInterval;
			// bounding box handling is pretty inefficient
			base.Update(gameTime);
		}

		// check collision method - static
		public static bool AreColliding(Object a, Object b)
		{
			return false;
		}
	}
}
