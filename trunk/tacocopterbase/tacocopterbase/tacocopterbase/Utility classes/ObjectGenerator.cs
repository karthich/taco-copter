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
	/// <typeparam name="T">Should be an Object constructed with a State2D (and a Game).</typeparam>
	class ObjectGenerator<T> : GameComponent
		where T : IGameComponent 
	{

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

	/// <summary>
	/// Generates Burrito missiles randomly between a specified range of
	/// Y-coordinates, at a somewhat random interval. 
	/// </summary>
	/// <typeparam name="T">Should be a burrito missile or powerup.</typeparam>
	class BurritoGenerator<T> : GameComponent
		where T : IGameComponent
	{
		private Func<State2D, Game, T> factory;
		protected float Interval;
		protected State2D GenState;
		protected Game thisGame;
		private float generateTime;

		// these ints define the vertical range
		// in which the Burritos will be randomly generated
		private int minY, maxY;
		Random random;

		public BurritoGenerator(Func<State2D, Game, T> f, State2D st, float i,
			int min, int max, Game g)
			: base(g)
		{
			factory = f;
			GenState = st;
			Interval = i;
			thisGame = g;
			minY = min;
			maxY = max;
			// don't want a burrito right away
			generateTime = 4f;
			random = new Random();
		}

		// with the current implementation, generates an object
		// at a maximum frequency of the refresh rate
		// TODO: fix this so there's no risk of overflow
		public override void Update(GameTime gameTime) 
		{
			if (gameTime.TotalGameTime.TotalSeconds > generateTime) 
			{
				var stateCopy = GenState.Copy();
				// change the Position.Y of this State2D to 
				// random y where minY <= y <= maxY
				stateCopy.Position = new Vector2(GenState.Position.X, random.Next(minY, maxY));
				thisGame.Components.Add(factory(stateCopy, thisGame));

				// don't generate at regular intervals
				float newInterval = Interval * ((float)random.NextDouble() + .5f);
				generateTime += newInterval;
			}
			base.Update(gameTime);
		}
	}


	/// <summary>
	/// Generates Customers at random but controlled intervals. 
	/// </summary>
	/// <typeparam name="T">Should be a customer.</typeparam>
	class CustomerGenerator<T> : GameComponent
		where T : IGameComponent
	{
		private Func<State2D, Game, T> factory;
		protected float IntervalMin;
		protected float IntervalMax;
		protected State2D GenState;
		protected Game thisGame;
		private float generateTime;
		Random random;

		public CustomerGenerator(Func<State2D, Game, T> f, State2D st, float minI,
			float maxI, Game g)
			: base(g)
		{
			factory = f;
			GenState = st;
			IntervalMin = minI;
			IntervalMax = maxI;
			thisGame = g;
			generateTime = 0f;
			random = new Random();
		}

		// with the current implementation, generates an object
		// at a maximum frequency of the refresh rate
		// TODO: fix this so there's no risk of overflow
		public override void Update(GameTime gameTime)
		{
			if (gameTime.TotalGameTime.TotalSeconds > generateTime)
			{
				thisGame.Components.Add(factory(GenState.Copy(), thisGame));
				// generate objects at an interval between IntervalMin and IntervalMax
				generateTime += (IntervalMax - IntervalMin) * (float)random.NextDouble() + IntervalMin;
			}
			base.Update(gameTime);
		}
	}
}
