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
    class Object : DrawableGameComponent
    {
		protected State2D State { get; set; }
		protected Texture2D sprite { get; set; }
		public SpriteBatch spriteBatch { get; set; }
		private Vector2 offset;
        protected Game thisGame;

		// commented this out because State2D does not
		// have a default constructor and it was an error
		
        
        public Object(Game g):base(g)
        { 
            State = new State2D();
            thisGame = g;
        }
         

        public Object(State2D s,Game g) :base(g)
        { 
            State = s;
            thisGame = g;
         }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
			// calculate offset
            LoadContent();
            base.Initialize();
        }

        public override void Draw(GameTime gametime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(sprite, State.Position - offset, null, Color.White, 0, 
				new Vector2(0, 0), 0.3f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        protected override void LoadContent()
        {
            sprite = thisGame.Content.Load<Texture2D>("black_box");
			offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
            base.LoadContent();
        }

        public virtual void Update(State2D s) { State = s; }
    }

    class Tacocopter : Object
    {
        private Vector2 offset;
        private TimeSpan lastFire;
        private int fireRate = 100;
        public List<Taco> tacos = new List<Taco>();

        public Tacocopter(State2D s, Game g)
            : base(g)
        {
            thisGame = g;
            State = s;
        }
        

        protected override void LoadContent()
        {
            sprite = this.Game.Content.Load<Texture2D>("black_box");
            offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
            base.LoadContent();
        }

        protected void FireTaco()
        {
           // if (gameTime.TotalGameTime.Subtract(lastFire).TotalMilliseconds >= fireRate)
            {
                Taco taco = null;
                taco = new Taco(thisGame, new State2D(State.Position.X, State.Position.Y, 0, 0, 0, 5, 0));
                
                tacos.Add(taco);
                Game.Components.Add(taco);

                //lastFire = gameTime.TotalGameTime;
            }
        }

        protected void CheckTacos()
        {
            List<Taco> removed = new List<Taco>();

            foreach (Taco taco in tacos)
            {
                if (taco.OffScreen)
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



        public void Update()
        {
            KeyboardState k = Keyboard.GetState();
            Vector2 nextPosition = new Vector2(0,0);

            
                if (k.IsKeyDown(Keys.Left) && State.Position.X > 100)
                {
                    nextPosition.X += -5;
                }
                if (k.IsKeyDown(Keys.Right) && State.Position.X <640)
                {
                    nextPosition.X += 5;
                }
                if (k.IsKeyDown(Keys.Up) && State.Position.Y > 100)
                {
                    nextPosition.Y += -5;
                }
                if (k.IsKeyDown(Keys.Down) && State.Position.Y < 360)
                {
                    nextPosition.Y += 5;
                }
                if (k.IsKeyDown(Keys.Space))
                {
                    FireTaco();

                }


            CheckTacos();
            foreach (Taco taco in tacos)
            {
                taco.Update();
            }
            State.Position += nextPosition;
            base.Update(State);
        }

    }

    class Taco : Object
    {
        private Vector2 offset;
        private bool offscreen = false;

        public Taco(Game g, State2D s)
            : base(g)
        {
            thisGame = g;
            State = s;
        }

        protected override void LoadContent()
        {
            sprite = this.Game.Content.Load<Texture2D>("black_box");
            offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
            base.LoadContent();
        }

        public bool OffScreen
        {
            get { return offscreen; }
        }

        public void Update()
        {
            State.Position += State.Velocity;

            if (State.Position.Y > 700)
            {
                offscreen = true;
            }

        }
            
    }

}
