using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1;

/* 
 * This file should contain the different types of Customers.
 */
namespace tacocopterbase {
	class Customer : Object {
		// maybe we should have a direct child of Object
		// that describes objects that need animated sprites
		// but this isn't too bad. 

        private const float Rotation = 0;
        private const float Scale = 0.5f;
        private const float Depth = 0.5f;
        private AnimatedTexture SpriteTexture;

		protected bool Satisfied { get; set; }

		// nothing special about a Customer yet
		public Customer(State2D s,Game g) : base(s, g) {
			Satisfied = false;
			SpriteTexture = new AnimatedTexture(
				new Vector2(52, 75), 0, .5f, .5f);
        }

		// load a generic person sprite
		protected override void LoadContent() {
			SpriteTexture.Load(thisGame.Content, "gb_walk_left", 6, 3, new Vector2(1, 6));
            
		}

        

		// draw the current frame given a ready SpriteBatch
		public override void Draw(SpriteBatch batch, GameTime gameTime)
		{
			SpriteTexture.DrawFrame(batch, State.Position);
		}
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SpriteTexture.UpdateFrame(elapsed);

            base.Update(gameTime);
        }
	}
}
