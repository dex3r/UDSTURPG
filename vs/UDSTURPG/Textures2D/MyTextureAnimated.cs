using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Textures2D
{
    public class MyTextureAnimated : MyTexture
    {
        private int framesCount;
        public int FramesCount
        {
            get { return framesCount; }
            set { framesCount = value; }
        }

        public MyTextureAnimated(string path, Rectangle sourceRectangle, float depthOfDrawing, int framesCount)
            : base(path, sourceRectangle, depthOfDrawing)
        {
            this.framesCount = framesCount;
        }

        public MyTextureAnimated(MyTexture sourceTexture, Rectangle sourceRectangle, float depthOfDrawing, int framesCount)
            : base(sourceTexture, sourceRectangle, depthOfDrawing)
        {
            this.framesCount = framesCount;
        }

        public override Rectangle GetCurrentSourceRectangle(int frame)
        {
            return new Rectangle((frame * sourceRectangle.Width) + sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height);
        }
    }
}
