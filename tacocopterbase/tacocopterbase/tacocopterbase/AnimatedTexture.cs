#region File Description
/**
 * Animated texture handles spritesheets and displays them on screen at a constant framerate
 * framecount = denote the number of frames in the sprite to show
 * TimePerFrame = reciprocal of FramesPerSec
 * Frame = Which frame number is the sprite currently drawing
 * TotalElapsed = Total Elapsed time to see how much time has passed. used to calculate which frame to display now
 * Paused = is the sprite animation paused?
 * SpriteSheetDims = how are the sprites arranged in the sprite sheet matrix dimension.
 * Rotation, Scale, Depth, Origin = define the position, rotation, scale and depth of the animated sprite
 * **/
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tacocopterbase
{
	public class AnimatedTexture
	{
		private int framecount;
		private Texture2D myTexture;
		private float TimePerFrame;
		private int Frame;
		private float TotalElapsed;
		private bool Paused;
		public int Radius { get; set; }
		public Vector2 Origin { get; set; }
		private int FrameWidth, FrameHeight;
		//MODIFICATION to the original code how the frames are arranged
		private Vector2 SpriteSheetDims;
		public float Rotation, Scale, Depth;

		public AnimatedTexture(Vector2 origin, float rotation, 
			float scale, float depth)
		{
			this.Origin = origin;
			this.Rotation = rotation;
			this.Scale = scale;
			this.Depth = depth;
			
		}
		public void Load(ContentManager content, string asset, 
			int frameCount, int framesPerSec,Vector2 spriteSheetDims)
		{
			framecount = frameCount;
			myTexture = content.Load<Texture2D>(asset);
			TimePerFrame = (float)1 / framesPerSec;
			Frame = 0;
			TotalElapsed = 0;
			Paused = false;
			//Modification from original code
			SpriteSheetDims = spriteSheetDims;
			// calculate the bounding radius
			FrameWidth = myTexture.Width / (int)SpriteSheetDims.Y;
			FrameHeight = myTexture.Height / (int)SpriteSheetDims.X;
			Radius = (int)((FrameWidth + FrameHeight) / 7f);
		}

		// class AnimatedTexture
		public void UpdateFrame(float elapsed)
		{
			if (Paused)
				return;
			TotalElapsed += elapsed;
			if (TotalElapsed > TimePerFrame)
			{
				Frame++;
				// Keep the Frame between 0 and the total frames, minus one.
				Frame = Frame % framecount;
				TotalElapsed -= TimePerFrame;
			}
		}

		// class AnimatedTexture
		public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
		{
			DrawFrame(batch, Frame, screenPos);
		}
		public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
		{
			//int FrameWidth = myTexture.Width / framecount;
			Rectangle sourcerect = new Rectangle(FrameWidth * (frame % (int)SpriteSheetDims.Y), 
				FrameHeight * ((frame / (int)SpriteSheetDims.Y) % (int)SpriteSheetDims.X),
				FrameWidth, FrameHeight);

			batch.Draw(myTexture, screenPos, sourcerect, Color.White,
				Rotation, Origin, Scale, SpriteEffects.None, Depth);
		}

		public bool IsPaused
		{
			get { return Paused; }
		}
		public void Reset()
		{
			Frame = 0;
			TotalElapsed = 0f;
		}
		public void Stop()
		{
			Pause();
			Reset();
		}
		public void Play()
		{
			Paused = false;
		}
		public void Pause()
		{
			Paused = true;
		}

	}
}
