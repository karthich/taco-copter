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
		protected bool Satisfied { get; set; }

		// nothing special about a Customer yet
		public Customer(State2D s,Game g) : base(s, g) {
        }

		// create a SpriteBatch for drawing and load the sprite
		public override void Initialize() {
			spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
			LoadContent();
			base.Initialize();
		}

		// load a generic person sprite
		protected override void LoadContent() {
			sprite = thisGame.Content.Load<Texture2D>("gb_walk");
			offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
			base.LoadContent();
		}
	}
}
