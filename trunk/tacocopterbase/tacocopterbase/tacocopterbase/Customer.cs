using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1;

namespace tacocopterbase {
	class Customer : Object {
		private Vector2 offset;

		// nothing special about a Customer yet
		public Customer(State2D s,Game g) : base(s, g) {
        }

		// load a generic person sprite
		protected override void LoadContent() {
			sprite = thisGame.Content.Load<Texture2D>("gb_walk.png");
			offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
			base.LoadContent();
		}

	}
}
