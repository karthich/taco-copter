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
	class Customer : AnimatedObject {
		// maybe we should have a direct child of Object
		// that describes objects that need animated sprites
		// but this isn't too bad. 

		public bool Satisfied { get; set; }

		// nothing special about a Customer yet
		public Customer(State2D s,Game g) : base(s, g) 
		{
			Satisfied = false;
		}

		// load a generic person sprite
		protected override void LoadContent() 
		{
			SpriteTexture.Load(thisGame.Content, "gb_walk_left", 6, 4, new Vector2(1, 6));
			base.LoadContent();
		}

		public Object CollideWith(Object obj)
		{
			var taco = obj as Taco;
			if (taco != null)
			{
				Satisfied = true;
				return new SpeechBubble(
					new State2D(State.Position + new Vector2(25, -62),
						new Vector2(0, 0), State.Velocity, 0), 1,
						"CHOMP", thisGame);
			}
			return null;
		}
	}

	class FastCustomer : Customer
	{
		public FastCustomer(State2D s, Game g) : base(s, g) { }

		protected override void LoadContent()
		{
			base.LoadContent();
			SpriteTexture.Load(thisGame.Content, "fastcustomer1", 8, 8, new Vector2(1, 8));
		}
	}
}
