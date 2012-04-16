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
	class AnimatedObject : Object
	{
		protected AnimatedTexture SpriteTexture;

		public AnimatedObject(State2D s, Game g) : base(s, g) 
		{
			SpriteTexture = new AnimatedTexture(0, .5f, .5f);
		}

		protected override void LoadContent()
		{
			// must load the resource with a child
			origin = SpriteTexture.Origin;
			boundWidth = (int)(origin.X*2f);
			boundHeight = (int)(origin.Y*2f);
		}

		public override void Draw(SpriteBatch batch, GameTime gameTime)
		{
			SpriteTexture.DrawFrame(batch, State.Position);
		}

		public override void Update(GameTime gameTime)
		{
			SpriteTexture.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
			base.Update(gameTime);
		}
	}
}
