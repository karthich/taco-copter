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
		protected int Radius; // for collisions

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
           //batch.Draw(sprite,Stat
            
		}

		protected override void LoadContent() {
			//sprite = thisGame.Content.Load<Texture2D>("black_box"); // the default sprite
			origin = new Vector2(sprite.Width, sprite.Height) / 2;
			Radius = (int)((sprite.Height + sprite.Width) / 7f);
			base.LoadContent();
		}

		// updates position of the Object based on velocity
		// and updates velocity of the Object based on acceleration
		public override void Update(GameTime gameTime) {
			float timeInterval = (float)gameTime.ElapsedGameTime.TotalSeconds;
			State.Position += State.Velocity * timeInterval;
			State.Velocity += State.Acceleration * timeInterval;
			// bounding box handling is pretty inefficient
			base.Update(gameTime);
		}

		// check collision method - static
		public static bool AreColliding(Object a, Object b)
		{
			return Vector2.Distance(a.State.Position + a.origin, b.State.Position + b.origin) < (a.Radius  + b.Radius);
		}
	}

	class Tacocopter : Object
	{
		private TimeSpan lastFire;
		private int fireRate = 500;
		public List<Taco> tacos = new List<Taco>();
		public int Speed { get; set; }

		private AnimatedTexture SpriteTexture;

		public Tacocopter(State2D s, Game g) : base(g)
		{
			thisGame = g;
			State = s;
			Speed = 300;
			SpriteTexture = new AnimatedTexture(new Vector2(225, 150), 0, .5f, .5f);
		}
		

		protected override void LoadContent()
		{
			SpriteTexture.Load(thisGame.Content, "main_helicopter", 5, 5, new Vector2(3, 2));
			origin = SpriteTexture.Origin;
			Radius = SpriteTexture.Radius;
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
			if (k.IsKeyDown(Keys.Right) && State.Position.X <950) {
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

		public Taco(State2D s, Game g) : base(s, g) { }

		protected override void LoadContent() {
			sprite = this.Game.Content.Load<Texture2D>("taco-sprite");
			//origin = new Vector2(sprite.Height / 2, sprite.Width / 2);
			base.LoadContent();
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
			base.LoadContent();
		}
	}
}
