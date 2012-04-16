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
	class Taco : Object
	{
		private bool offscreen;
		public bool Offscreen
		{
			get { return offscreen; }
		}

		public Taco(State2D s, Game g) : base(s, g) { }

		protected override void LoadContent()
		{
			sprite = this.Game.Content.Load<Texture2D>("taco-sprite");
			base.LoadContent();
		}

		public override void Update(GameTime gameTime)
		{
			if (State.Position.Y > 700)
			{
				offscreen = true;
			}
			base.Update(gameTime);
		}
	}
}
