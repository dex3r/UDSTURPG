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

        public static MyTexture Wall = new MyTexture(@"tiled\Tileset-Wall", new Rectangle(0, 0, 31, 55), 0.7f);
        public static MyTexture Floor = new MyTexture(@"tiled\Tileset-Floor", new Rectangle(0, 0, 31, 31));
        public static MyTextureAnimated PlayerLordUpShooting = new MyTextureAnimated(@"art\player\lord_lard_sheet", new Rectangle(0, 0, 31, 31), 0.6f, 6);

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

        public MyTexture(string path, Rectangle sourceRectangle)
        {
            Textures.Add(this);
            this.texturePath = path;
            this.sourceRectangle = sourceRectangle;
            depthOfDrawing = 0.5f;
        }
        public MyTexture(string path, Rectangle sourceRectangle, float depthOfDrawing) : this(path, sourceRectangle)
        {
            this.depthOfDrawing = depthOfDrawing;
        }

        public MyTexture(MyTexture sourceTexture, Rectangle sourceRectangle, float depthOfDrawing)
        {
            this.texturePath = sourceTexture.texturePath;
            this.texture = sourceTexture.Texture;
        }

        public void Load(ContentManager contentManager)
        {
            if(this.texture == null)
            {
                texture = contentManager.Load<Texture2D>(@"gfx\" + texturePath);
            }
        }

        public virtual Rectangle GetCurrentSourceRectangle(int frame)
        {
            return sourceRectangle;
        }
    }
}
