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
	class Object : DrawableGameComponent {
		public State2D State { get; set; }
		protected Texture2D sprite;
		protected SpriteBatch spriteBatch { get; set; }
		protected Vector2 origin;
		protected Game thisGame;
		public Rectangle Box { get; set; } // the bounding box

		public Object(Game g) : base(g) {
			State = new State2D();
			thisGame = g;
		}

		public Object(State2D s, Game g) : base(g) {
			State = s;
			thisGame = g;
		}

		public override void Initialize() {
			spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
			LoadContent();
			base.Initialize();
		}

		// draw the Object at State.Position with origin
		public virtual void Draw(SpriteBatch batch, GameTime gametime) {
			batch.Draw(sprite, State.Position, null, Color.White, 0,
				origin, 0.3f, SpriteEffects.None, 0);
		}

		protected override void LoadContent() {
			sprite = thisGame.Content.Load<Texture2D>("black_box"); // the default sprite
			origin = new Vector2(sprite.Height, sprite.Width) / 2;
			Box = new Rectangle((int)(State.Position.X + sprite.Width / 10),
				(int)(State.Position.Y + sprite.Height / 10),
				(int)(sprite.Width * .8f), (int)(sprite.Height * .8f));
			base.LoadContent();
		}

		// updates position of the Object based on velocity
		// and updates velocity of the Object based on acceleration
		public override void Update(GameTime gameTime) {
			float timeInterval = (float)gameTime.ElapsedGameTime.TotalSeconds;
			State.Position += State.Velocity * timeInterval;
			State.Velocity += State.Acceleration * timeInterval;
			// bounding box handling is pretty inefficient
			if (!Box.IsEmpty)
			{
				Box = new Rectangle((int)(State.Position.X + sprite.Width / 10),
				(int)(State.Position.Y + sprite.Height / 10),
				(int)(sprite.Width * .8f), (int)(sprite.Height * .8f));
			}
			base.Update(gameTime);
		}

		// check collision method - static
		public static bool AreColliding(Object a, Object b)
		{
			return Rectangle.Intersect(a.Box, b.Box) != Rectangle.Empty;
		}
	}

	class Tacocopter : Object
	{
		private TimeSpan lastFire;
		private int fireRate = 500;
		public List<Taco> tacos = new List<Taco>();
		public int Speed { get; set; }

		private AnimatedTexture SpriteTexture;

		public Tacocopter(State2D s, Game g)
			: base(g)
		{
			thisGame = g;
			State = s;
			Speed = 200;

			SpriteTexture = new AnimatedTexture(Vector2.Zero, 0, .5f, .5f);
		}
		

		protected override void LoadContent()
		{
			SpriteTexture.Load(thisGame.Content, "main_helicopter", 5, 5, new Vector2(3, 2));
		}

		protected void FireTaco(GameTime gameTime)
		{
			if (gameTime.TotalGameTime.Subtract(lastFire).TotalMilliseconds >= fireRate)
			{
				Taco taco = null;
				taco = new Taco(thisGame, new State2D(State.Position.X + 120, State.Position.Y + 125,
					0, 400, State.Velocity.X, 200, 0));
				
				tacos.Add(taco);
				Game.Components.Add(taco);

				lastFire = gameTime.TotalGameTime;
			}
		}

		protected void CheckTacos() 
		{
			List<Taco> removed = new List<Taco>();

			foreach (Taco taco in tacos) {
				if (taco.Offscreen) {
					Game.Components.Remove(taco);
					removed.Add(taco);
				}
			}
			foreach (Taco taco in removed) {
				tacos.Remove(taco);
			}
		}

		public override void Draw(SpriteBatch batch,GameTime gameTime)
		{
			SpriteTexture.DrawFrame(batch, State.Position);
		}

		public override void Update(GameTime gameTime) {

			KeyboardState k = Keyboard.GetState();
			Vector2 nextVelocity = new Vector2(0,0);
			
			if (k.IsKeyDown(Keys.Left) && State.Position.X > 10) {
				nextVelocity.X += -Speed;
			}
			if (k.IsKeyDown(Keys.Right) && State.Position.X <640) {
				nextVelocity.X += Speed;
			}
			if (k.IsKeyDown(Keys.Up) && State.Position.Y > 10) {
				nextVelocity.Y += -Speed;
			}
			if (k.IsKeyDown(Keys.Down) && State.Position.Y < 360) {
				nextVelocity.Y += Speed;
			}

			State.Velocity = nextVelocity;

			if (k.IsKeyDown(Keys.Space)){
				FireTaco(gameTime);
			}

			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			SpriteTexture.UpdateFrame(elapsed);
		   
			// delete offscreen tacos
			CheckTacos();

			base.Update(gameTime);
		}
	}

	class Taco : Object {
		private bool offscreen;
		public bool Offscreen { 
			get { return offscreen;} 
		}

		public Taco(Game g, State2D s)
			: base(s, g) {
		}

		protected override void LoadContent() {
			sprite = this.Game.Content.Load<Texture2D>("taco-sprite");
			origin = new Vector2(sprite.Height / 2, sprite.Width / 2);
			
		}

		public override void Update(GameTime gameTime) {
			if (State.Position.Y > 700) {
				offscreen = true;
			}
			base.Update(gameTime);
		}
	}

	/// <summary>
	/// The way I have Object generation set up, many of these
	/// classes will just be placeholders for sprites, essentially. 
	/// </summary>
	class Burrito : Object
	{
		public Burrito(State2D s, Game g) : base(s, g) { }

		protected override void LoadContent() 
		{
			sprite = this.Game.Content.Load<Texture2D>("burritomissile");
			origin = new Vector2(sprite.Height / 2, sprite.Width / 2);
		}
	}
}
