using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1;

namespace tacocopterbase
{
	class Particle : DrawableGameComponent
	{
		protected State2D State;
        protected AnimatedTexture SpriteTexture;
		protected Game thisGame;

		public Particle(State2D s, Game g) : base(g)
		{
			State = s;
			thisGame = g;
        }

		// load a generic particle animation (explosion)
		protected override void LoadContent() 
		{
			SpriteTexture = new AnimatedTexture(0, 0.5f, 0.5f);
			SpriteTexture.Load(thisGame.Content, "generic_explosion", 12, 5, new Vector2(4, 3));
		}

		// draw the current frame given a ready SpriteBatch
		public void Draw(SpriteBatch batch, GameTime gameTime)
		{
			SpriteTexture.DrawFrame(batch, State.Position);
		}

		// update the frame number
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SpriteTexture.UpdateFrame(elapsed);
            base.Update(gameTime);
        }
	}
}
