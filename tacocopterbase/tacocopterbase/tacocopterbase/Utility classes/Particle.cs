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
	class Particle : Object
	{
		private float BirthTime;
		private float LifeTime;
		public bool DestroyThis;
		protected SpriteFont Arial;

		/// <param name="time">Lifetime of the object in seconds</param>
		public Particle(State2D s, int time, Game g)
			: base(s, g)
		{
			LifeTime = time;
			BirthTime = 0;
			DestroyThis = false;
			Arial = g.Content.Load<SpriteFont>("Arial");
		}

		public override void Update(GameTime gameTime)
		{
			BirthTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (BirthTime > LifeTime)
				DestroyThis = true;
			base.Update(gameTime);
		}
	}

	class SpeechBubble : Particle
	{
		string speechText;
		public SpeechBubble(State2D s, int time, string text, Game g) : base(s, time, g) 
		{
			speechText = text;
		}

		protected override void LoadContent()
		{
			sprite = this.Game.Content.Load<Texture2D>("speech");
			base.LoadContent();
		}

		public override void Draw(SpriteBatch batch, GameTime gameTime)
		{
			base.Draw(batch, gameTime);
			batch.DrawString(Arial, speechText, State.Position + new Vector2(-30, -12), Color.Black);
		}
	}

	class ThoughtBubble : Particle
	{
		string speechText;
		public ThoughtBubble(State2D s, int time, string text, Game g) : base(s, time, g)
		{
			speechText = text;
		}

		protected override void LoadContent()
		{
			sprite = this.Game.Content.Load<Texture2D>("thought");
			base.LoadContent();
		}

		public override void Draw(SpriteBatch batch, GameTime gameTime)
		{
			base.Draw(batch, gameTime);
			batch.DrawString(Arial, speechText, State.Position + new Vector2(-30, -12), Color.Black);
		}
	}

	class Explosion : Particle
	{
		public Explosion(State2D s, int time, Game g) : base(s, time, g) { }

		protected override void LoadContent()
		{
			sprite = this.Game.Content.Load<Texture2D>("generic_explosion");
			base.LoadContent();
		}
	}
}
