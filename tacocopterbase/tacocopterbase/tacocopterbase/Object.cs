using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
		/*
        public Object(Game g):base(g)
        { 
            currentState = new State2D();
            thisGame = g;
        }*/

        public Object(State2D s,Game g) :base(g)
        { 
            State = s;
            thisGame = g;
         }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
			// calculate offset
			offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
            LoadContent();
            base.Initialize();
        }

        public override void Draw(GameTime gametime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(sprite, State.Position - offset, null, Color.White, 0, 
				new Vector2(0, 0), (Single)0.3, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        protected override void LoadContent()
        {
            sprite = thisGame.Content.Load<Texture2D>("black_box");
            base.LoadContent();
        }

        public void Update(State2D s) { State = s; }
    }
}
