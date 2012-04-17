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
	/// <summary>
	/// The way I have Object generation set up, many of these
	/// classes will just be placeholders for sprites, essentially. 
	/// </summary>
	public class Burrito : Object
	{
		public Burrito(State2D s, Game g) : base(s, g) {
            State = s;
        }

		protected override void LoadContent()
		{
			sprite = this.Game.Content.Load<Texture2D>("burritomissile");
			base.LoadContent();
		}
	}
}
