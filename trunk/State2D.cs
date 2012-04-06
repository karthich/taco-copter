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

        Vector2 Position;
        double mass;
        private Vector2 acceleration;
        private Vector2 velocity;

        public Vector2 Position1
        {
            get { return Position; }
            set { Position = value; }
        }
        
        

        public double Mass
        {
            get { return mass; }
            set { mass = value; }
        }
        
        
        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }
        

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public State2D(Vector2 pos, Vector2 acc, Vector2 vel, double mass)
        {
            Mass = mass;
            Position = pos;
            Acceleration = acc;
            Velocity = vel;
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