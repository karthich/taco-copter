using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1;

namespace tacocopterbase
{
	class Particle : Object
	{
		private float BirthTime;
		private float LifeTime;

		public Particle(State2D s, float time, Game g)
			: base(s, g)
		{
			LifeTime = time;
		}
	}
}
