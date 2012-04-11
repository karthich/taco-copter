using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1;

namespace tacocopterbase {
	// generates one type of object at a specific interval
	// will generate the Object at the location specified in the Object
	class ObjectGenerator : GameComponent {

		// interval in seconds at which to generate the object
		// object to be generated at interval Interval
		// need thisGame to add Objects to the game
		protected float Interval;
		protected Object Obj;
		protected Game thisGame;
		
		// this variable is used to determine when to generate the Obj
		// when TotalGameTime > generateTime, add a copy of Obj to the game
		// increment generateTime by Interval when this happens
		private float generateTime;

		// create a new ObjectGenerator at location l,
		// to generate Object o at an interval i
		public ObjectGenerator(Object o, float i, Game g) : base(g) {
			Obj = o;
			Interval = i;
			thisGame = g;
			// generate an object right away
			generateTime = 0f;
		}

		// with the current implementation, generates an object
		// at a maximum frequency of the refresh rate
		// TODO: fix this so there's no risk of overflow
		public override void Update(GameTime gameTime) {
			if (gameTime.TotalGameTime.TotalSeconds > generateTime) {
				thisGame.Components.Add(Obj.Copy());
				generateTime += Interval;
			}
			base.Update(gameTime);
		}
		
		// default methods that don't do anything
		public override void Initialize() {
			base.Initialize();
		}
	}
}
