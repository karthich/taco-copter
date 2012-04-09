using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tacocopterbase
{
    class Object : DrawableGameComponent
    {
        private State2D s0;

        
        virtual private Texture2D sprite;
        private SpriteBatch spriteBatch;

        
        protected Game thisGame;

        public Object(Game g):base(g)
        { 
            s0 = new State2D();
            thisGame = g;
        }

        public Object(State2D s,Game g) :base(g)
        { 
            s0 = s;
            thisGame = g;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            base.Initialize();
        }
        virtual void Draw(GameTime gametime)
        {
            Vector2 offset = new Vector2(sprite.Height / 2, sprite.Width / 2);
            spriteBatch.Begin();
            spriteBatch.Draw(sprite, s0.Position1, null, Color.White, 0, new Vector2(0, 0), (Single)0.3, SpriteEffects.None, 0);
            spriteBatch.End();
        }
        protected override void LoadContent()
        {
            sprite = thisGame.Content.Load<Texture2D>("black_box");
            base.LoadContent();
        }

        public void Update(State2D s)
        {
            s0 = s;
        }

        protected Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public SpriteBatch Spritebatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        protected State2D S0
        {
            get { return s0; }
            set { s0 = value; }
        }
    }
}
