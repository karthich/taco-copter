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
	class Tacocopter : AnimatedObject 
	{
		private TimeSpan lastFire;
		private int fireRate = 500;
		public List<Taco> tacos = new List<Taco>();
		public int Speed { get; set; }

		public Tacocopter(State2D s, Game g)
			: base(s, g)
		{
            State = s;
			Speed = 400;
		}
		
		protected override void LoadContent()
		{
			SpriteTexture.Load(thisGame.Content, "main_helicopter1", 5, 5, new Vector2(3, 2));
			base.LoadContent();
		}

		protected void FireTaco(GameTime gameTime)
		{
			if (gameTime.TotalGameTime.Subtract(lastFire).TotalMilliseconds >= fireRate)
			{
				Taco taco = null;
				taco = new Taco(new State2D(State.Position.X + 10, State.Position.Y + 50,
					0, 800, State.Velocity.X, 200, 0), thisGame);

                

				tacos.Add(taco);
				Game.Components.Add(taco);

				lastFire = gameTime.TotalGameTime;
			}
		}

		protected void CheckTacos()
		{
			List<Taco> removed = new List<Taco>();

			foreach (Taco taco in tacos)
			{
				if (taco.Offscreen)
				{
					Game.Components.Remove(taco);
					removed.Add(taco);
				}
			}
			foreach (Taco taco in removed)
			{
				tacos.Remove(taco);
			}
		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState k = Keyboard.GetState();
			Vector2 nextAcc = new Vector2(0, 0);
			Vector2 nextVelocity = new Vector2(State.Velocity.X, State.Velocity.Y);

			if (k.IsKeyDown(Keys.Left))
			{
				nextAcc.X += -Speed/2;
				nextVelocity.X += -Speed / 20;
			}
			if (k.IsKeyDown(Keys.Right))
			{
				nextAcc.X += Speed/2;
				nextVelocity.X += Speed / 20;
			}
			if (k.IsKeyDown(Keys.Up))
			{
				nextAcc.Y += -Speed/2;
				nextVelocity.Y += -Speed / 20;
			}
			if (k.IsKeyDown(Keys.Down))
			{
				nextAcc.Y += Speed/2;
				nextVelocity.Y += Speed / 20;
			}

			State.Acceleration = nextAcc;
			State.Velocity = nextVelocity;

			nextVelocity = new Vector2(State.Velocity.X, State.Velocity.Y);
			if (Math.Abs(State.Velocity.X) > Speed)
				nextVelocity.X = State.Velocity.X / Math.Abs(State.Velocity.X) * Speed;
			if (Math.Abs(State.Velocity.Y) > Speed)
				nextVelocity.Y = State.Velocity.Y / Math.Abs(State.Velocity.Y) * Speed;
			State.Velocity = nextVelocity;

			if (k.IsKeyDown(Keys.Space))
			{
				FireTaco(gameTime);
			}

			// delete offscreen tacos
			CheckTacos();

			// update position
			base.Update(gameTime);

			// constrain position
			Vector2 nextPosition = new Vector2(State.Position.X, State.Position.Y);
			nextVelocity = new Vector2(State.Velocity.X, State.Velocity.Y);
			if (State.Position.X < 100)
			{
				nextPosition.X = 100;
				nextVelocity.X = 0;
			}
			if (State.Position.X > 960)
			{
				nextPosition.X = 960;
				nextVelocity.X = 0;
			}
			if (State.Position.Y < 140)
			{
				nextPosition.Y = 140;
				nextVelocity.Y = 0;
			}
			if (State.Position.Y > 500)
			{
				nextPosition.Y = 500;
				nextVelocity.Y = 0;
			}
			State.Position = nextPosition;
			State.Velocity = nextVelocity;
		}
	}
}