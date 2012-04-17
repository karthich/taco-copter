using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tacocopterbase
{
    public class HealthBar
    {

		private Vector2 origin;
        private Texture2D myTexture;
        //private int screenheight;
        private Vector2 screenDims;

        public HealthBar(){}
        public void Load(GraphicsDevice device, Texture2D healthBarTexture)
        {
            myTexture = healthBarTexture;
            screenDims.Y = device.Viewport.Height;
            screenDims.X = device.Viewport.Width;

            origin = new Vector2(myTexture.Width / 2, myTexture.Height / 2);
        }

        public void Draw(SpriteBatch batch,Player p)
        {

            batch.Draw(myTexture, new Rectangle((int)screenDims.X / 2 - myTexture.Width / 2,
                       30, myTexture.Width, 44), new Rectangle(0, 45, myTexture.Width, 44), Color.Gray);


            //Draw the current health level based on the current Health
            batch.Draw(myTexture, new Rectangle((int)screenDims.X/2 - myTexture.Width / 2,
                 30, (int)(myTexture.Width * ((double)p.Health / 100)), 44),
                 new Rectangle(0, 45, myTexture.Width, 44), Color.Red);

            //Draw the box around the health bar
            batch.Draw(myTexture, new Rectangle((int) screenDims.X/ 2 - myTexture.Width / 2,

                30, myTexture.Width, 44), new Rectangle(0, 0, myTexture.Width, 44), Color.White);
        }

        public void Update(GameTime gametime, Player p)
        {
            // At present not in use
        }
            
    }
}