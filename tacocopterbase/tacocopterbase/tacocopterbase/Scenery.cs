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
	class Sidewalk : Object
	{
		public Sidewalk(State2D s, Game g) : base(s, g) { }

		protected override void LoadContent()
		{
			sprite = thisGame.Content.Load<Texture2D>("badsidewalk1");
			origin = new Vector2(sprite.Height / 2, sprite.Width / 2);
		}
	}
}
