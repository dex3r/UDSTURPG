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
        public static MyTexture PlayerLordLard = new MyTexture(@"art\player\lord_lard_sheet", new Rectangle(0, 0, 32, 32), 0.6f, 6);
        public static MyTexture Bullet = new MyTexture(@"art\effects\bullet", new Rectangle(0, 0, 16, 16), 0.55f);
        public static MyTexture MobMummy = new MyTexture(@"art\mob\enemy_mummy_anim_48", new Rectangle(0, 0, 48, 48), 0.60025f, 4, 5); //! 0025 to wielkość obrazka w przybliżeniu


        public static void LoadAll(ContentManager cm)
        {
            foreach (MyTexture mt in textures)
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

        private MyTexture(Rectangle sourceRectangle, float depthOfDrawing, int framesCount, int animationSpeed) : this()
        {
            this.sourceRectangle = sourceRectangle;
            this.depthOfDrawing = depthOfDrawing;
            this.framesCount = framesCount;
            this.animationSpeed = animationSpeed;
        }

        public MyTexture(string path, Rectangle sourceRectangle, float depthOfDrawing = 0, int framesCount = 1, int animationSpeed = 8) : this(sourceRectangle, depthOfDrawing, framesCount, animationSpeed)
        {
            this.texturePath = path;
            this.sourceRectangle = sourceRectangle;
            this.depthOfDrawing = depthOfDrawing;
            this.framesCount = framesCount;
            this.animationSpeed = animationSpeed;
        }

        public MyTexture(MyTexture sourceTexture, Rectangle sourceRectangle, float depthOfDrawing = 0, int framesCount = 1, int animationSpeed = 8) : this(sourceRectangle, depthOfDrawing, framesCount, animationSpeed)
        {
            this.texturePath = sourceTexture.texturePath;
            this.texture = sourceTexture.texture;
            this.sourceRectangle = sourceRectangle;
            this.depthOfDrawing = depthOfDrawing;
            this.framesCount = framesCount;
        }


        public void Load(ContentManager contentManager)
        {
            if (this.texture == null)
            {
                texture = contentManager.Load<Texture2D>(@"gfx\" + texturePath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="textureId">Dla wielu tekstur tego samego obiektu w jednym pliku, np. wiele animacji gracza.</param>
        /// <param name="animationSpeed"></param>
        /// <returns></returns>
        public virtual Rectangle GetCurrentSourceRectangle(int frame, int textureId = 0, int animationSpeed = -1)
        {
            if(animationSpeed == -1)
            {
                animationSpeed = this.animationSpeed;
            }
            return new Rectangle(((frame / animationSpeed) * sourceRectangle.Width) + sourceRectangle.X, sourceRectangle.Y + (textureId * sourceRectangle.Height), sourceRectangle.Width, sourceRectangle.Height);
        }
    }
}
