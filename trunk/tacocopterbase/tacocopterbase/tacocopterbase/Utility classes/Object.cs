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
	public class Object : DrawableGameComponent {
		public State2D State { get; set; }
		protected Texture2D sprite;
		protected SpriteBatch spriteBatch { get; set; }
		protected Vector2 origin;
		protected Game thisGame;
		protected int boundWidth, boundHeight;

		public Object(Game g) : base(g) {
			State = new State2D();
			thisGame = g;
		}

		public Object(State2D s, Game g) : base(g) {
			State = s;
			thisGame = g;
		}

		// draw the Object at State.Position with origin
		public virtual void Draw(SpriteBatch batch, GameTime gametime) {
			batch.Draw(sprite, State.Position, null, Color.White, 0,
				origin, 0.3f, SpriteEffects.None, 0);
		}

		// you MUST load a sprite before calling this parent function
		protected override void LoadContent() {
			origin = new Vector2(sprite.Width, sprite.Height) / 2;
			boundWidth = sprite.Width;
			boundHeight = sprite.Height;
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
			Rectangle aBox = new Rectangle(
				(int)(a.State.Position.X + .2f*a.boundWidth),
				(int)(a.State.Position.Y + .2f*a.boundHeight), 
				(int)(a.boundWidth*.6f), 
				(int)(a.boundHeight*.6f));
			Rectangle bBox = new Rectangle(
				(int)(b.State.Position.X + .2f * b.boundWidth),
				(int)(b.State.Position.Y + .2f * b.boundHeight),
				(int)(b.boundWidth * .6f),
				(int)(b.boundHeight * .6f));
			return Rectangle.Intersect(aBox, bBox) != Rectangle.Empty;
		}
	}
}
