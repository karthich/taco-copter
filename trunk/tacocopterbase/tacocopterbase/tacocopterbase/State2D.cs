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
 * Vector2 Position = (x,y) position of the object
 * Double Mass = mass of the object
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
		public State2D()
        {
            Mass = 0;
            Position = NULL_VECTOR;
            Acceleration = NULL_VECTOR;
            Velocity = NULL_VECTOR;
        }

		// constructor that takes Vector2s
        public State2D(Vector2 pos, Vector2 acc, Vector2 vel, double mass)
        {
            Mass = mass;
            Position = pos;
            Acceleration = acc;
            Velocity = vel;
        }

		// another, simpler constructor
		public State2D(float posX, float posY, float accX, float accY, float velX, 
			float velY, double mass)
		{
			Mass = mass;
			Position = new Vector2(posX, posY);
			Acceleration = new Vector2(accX, accY);
			Velocity = new Vector2(velX, velY);
		}

        public void Update(Vector2 pos, Vector2 acc, Vector2 vel, double mass)
        {
            Mass = mass;
            Position = pos;
            Acceleration = acc;
            Velocity = vel;            
        }
    }
}
