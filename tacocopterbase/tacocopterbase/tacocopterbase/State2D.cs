using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/**Encodes the State Variables of the object.
 * Double Mass = mass of the object
 * Vector2 Position = (x,y) position of the object
 * Vec2 Acceleration = acceleration of the object
 * Vec2 Velocity = velocity of the object
 * **/
namespace WindowsGame1
{
	public class State2D
	{
		public double Mass { get; set; }
		public Vector2 Position { get; set; }
		public Vector2 Acceleration { get; set; }
		public Vector2 Velocity { get; set; }
		static Vector2 NULL_VECTOR = new Vector2(0,0);

		// default constructor
		public State2D() {
			Mass = 0;
			Position = NULL_VECTOR;
			Acceleration = NULL_VECTOR;
			Velocity = NULL_VECTOR;
		}

		// constructor that takes Vector2s
		public State2D(Vector2 pos, Vector2 acc, Vector2 vel, double mass) {
			Mass = mass;
			Position = pos;
			Acceleration = acc;
			Velocity = vel;
		}

		// a simpler constructor that directly takes floats
		public State2D(float posX, float posY, float accX, float accY, float velX, 
			float velY, double mass) {
			Mass = mass;
			Position = new Vector2(posX, posY);
			Acceleration = new Vector2(accX, accY);
			Velocity = new Vector2(velX, velY);
		}

		// constructor for static State2D
		public State2D(float posX, float posY) {
			Mass = 0;
			Position = new Vector2(posX, posY);
			Acceleration = new Vector2(0, 0);
			Velocity = new Vector2(0, 0);
		}

		// returns a new copy of this state
		public State2D Copy()
		{
			return new State2D(Position, Acceleration, Velocity, Mass);
		}
	}
}
