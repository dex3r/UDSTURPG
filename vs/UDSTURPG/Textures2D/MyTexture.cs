using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG.Textures2D
{
    public class MyTexture
    {
        #region static
        private static List<MyTexture> textures = new List<MyTexture>(30);
        public static List<MyTexture> Textures
        {
            get { return MyTexture.textures; }
        }

        public static MyTexture Wall = new MyTexture(@"tiled\Tileset-Wall", new Rectangle(0, 0, 32, 55), 0.7f);
        public static MyTexture Floor = new MyTexture(@"tiled\Tileset-Floor", new Rectangle(0, 0, 32, 32));
        public static MyTexture PlayerLordUpShooting = new MyTexture(@"art\player\lord_lard_sheet", new Rectangle(0, 0, 32, 32), 0.6f, 6);

        public static void LoadAll(ContentManager cm)
        {
            foreach(MyTexture mt in textures)
            {
                mt.Load(cm);
            }
        }
        #endregion

        private string texturePath;

        private Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
        }

        protected Rectangle sourceRectangle;

        private float depthOfDrawing;
        public float DepthOfDrawing
        {
            get { return depthOfDrawing; }
        }

        private int framesCount;
        public int FramesCount
        {
            get { return framesCount; }
        }

        private int animationSpeed;
        /// <summary>
        /// Liczba ticków na klatkę
        /// </summary>
        public int AnimationSpeed
        {
            get { return animationSpeed; }
        }

        private MyTexture()
        {
            Textures.Add(this);
        }

        public MyTexture(string path, Rectangle sourceRectangle, float depthOfDrawing = 0, int framesCount = 0, int animationSpeed = 8) : this()
        {
            this.texturePath = path;
            this.sourceRectangle = sourceRectangle;
            this.depthOfDrawing = depthOfDrawing;
            this.framesCount = framesCount;
            this.animationSpeed = animationSpeed;
        }

        public MyTexture(MyTexture sourceTexture, Rectangle sourceRectangle, float depthOfDrawing = 0, int framesCount = 0) : this()
        {
            this.texturePath = sourceTexture.texturePath;
            this.texture = sourceTexture.texture;
            this.sourceRectangle = sourceRectangle;
            this.depthOfDrawing = depthOfDrawing;
            this.framesCount = framesCount;
        }


        public void Load(ContentManager contentManager)
        {
            if(this.texture == null)
            {
                texture = contentManager.Load<Texture2D>(@"gfx\" + texturePath);
            }
        }

        public virtual Rectangle GetCurrentSourceRectangle(int frame, int animationSpeed = -1)
        {
            if(animationSpeed == -1)
            {
                animationSpeed = this.animationSpeed;
            }
            if (framesCount > 0)
            {
                return new Rectangle(((frame / animationSpeed) * sourceRectangle.Width) + sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height);
            }
            else
            {
                return sourceRectangle;
            }
        }
    }
}
