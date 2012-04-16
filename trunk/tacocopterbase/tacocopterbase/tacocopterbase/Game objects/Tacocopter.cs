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
			Speed = 300;
		}

		protected override void LoadContent()
		{
			SpriteTexture.Load(thisGame.Content, "main_helicopter", 5, 5, new Vector2(3, 2));
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
			Vector2 nextVelocity = new Vector2(0, 0);

			if (k.IsKeyDown(Keys.Left) && State.Position.X > 10)
			{
				nextVelocity.X += -Speed;
			}
			if (k.IsKeyDown(Keys.Right) && State.Position.X < 950)
			{
				nextVelocity.X += Speed;
			}
			if (k.IsKeyDown(Keys.Up) && State.Position.Y > 10)
			{
				nextVelocity.Y += -Speed;
			}
			if (k.IsKeyDown(Keys.Down) && State.Position.Y < 360)
			{
				nextVelocity.Y += Speed;
			}
			State.Velocity = nextVelocity;
			if (k.IsKeyDown(Keys.Space))
			{
				FireTaco(gameTime);
			}

			// delete offscreen tacos
			CheckTacos();

			base.Update(gameTime);
		}
	}
}