using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1;

namespace tacocopterbase {

	/// <summary>
	/// An ObjectGenerator generates one type of object at a specific interval.
	/// This basic version generates objects with constructors that take a State2D only. 
	/// </summary>
	class ObjectGenerator<T> : GameComponent
	where T : IGameComponent {

		// this code makes T's constructor with args work correctly
		private Func<State2D, Game, T> factory;

		// interval in seconds at which to generate the object
		// state of object to be generated at interval Interval
		// need thisGame to add Objects to the game
		protected float Interval;
		protected State2D GenState;
		protected Game thisGame;
		
		// this variable is used to determine when to generate the Obj
		// when TotalGameTime > generateTime, add a copy of Obj to the game
		// increment generateTime by Interval when this happens
		private float generateTime;

		// create a new ObjectGenerator to generate an object at interval i
		public ObjectGenerator(Func<State2D, Game, T> f, State2D st, float i, Game g)
			: base(g)
		{
			factory = f;
			GenState = st;
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
				// must deep copy the state for each new object
				thisGame.Components.Add(factory(GenState.Copy(), thisGame));
				generateTime += Interval;
			}
			base.Update(gameTime);
		}
	}
}
