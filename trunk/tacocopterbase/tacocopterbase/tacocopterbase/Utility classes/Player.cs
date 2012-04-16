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
    public class Player : DrawableGameComponent
    {
		public int Score { get; set; }
		public int Health { get; set; }
		public int CustomerSatisfaction { get; set; }
		private Texture2D healthBar;
		private Game g;
		private bool youLose;
		private SpriteBatch batch;

		public Player(Game game) : base(game)
		{
			g = game;
			youLose = false;
            Health = 100;
            CustomerSatisfaction = 0;
		}

		public override void Initialize()
		{
			healthBar = Game.Content.Load<Texture2D>("HealthBar2");
			batch = new SpriteBatch(g.GraphicsDevice);
			base.Initialize();
		}

		public override void Draw(GameTime gameTime)
		{
			batch.Begin();
			batch.Draw(healthBar, new Rectangle(g.Window.ClientBounds.Width / 2 - healthBar.Width / 2,
				 30, healthBar.Width, 44), new Rectangle(0, 45, healthBar.Width, 44), Color.Gray);

			//Draw the current health level based on the current Health
			batch.Draw(healthBar, new Rectangle(g.Window.ClientBounds.Width / 2 - healthBar.Width / 2,
				 30, (int)(healthBar.Width * ((double)Health / 100)), 44),
				 new Rectangle(0, 45, healthBar.Width, 44), Color.Red);

			//Draw the box around the health bar
			batch.Draw(healthBar, new Rectangle(g.Window.ClientBounds.Width / 2 - healthBar.Width / 2,
				30, healthBar.Width, 44), new Rectangle(0, 0, healthBar.Width, 44), Color.White);

			// draw the game over condition
			if (youLose)
			{
				string endGame = "GAME OVER";
				batch.DrawString(g.Content.Load<SpriteFont>("gameOver"), endGame, new Vector2(450, 80), Color.Blue);
			}
            // draw customer satisfaction label
            string health = "Customer Satisfaction:";
            batch.DrawString(g.Content.Load<SpriteFont>("playerScore"), health, new Vector2(410, 7), Color.Blue);

			// draw the score
			string score = "Profit: $" + Score.ToString();
			batch.DrawString(g.Content.Load<SpriteFont>("playerScore"), score, new Vector2(1000, 25), Color.Blue);
			batch.End();

			base.Draw(gameTime);
		}

		public void Lose()
		{
			youLose = true;
		}

		public void UnLose()
		{
			if (youLose)
			{
				youLose = false;
				Score = 0;
				g.Components.Add(new Tacocopter(new State2D(400, 200), g));
			}
		}
    }
}