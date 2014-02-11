using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;
using RPG.Main;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPG.Entities
{
    public class Entity
    {
        // Granica, za którą obiekt znika
        public const int BORDER = 100;
        public const int MBORDER = -BORDER;

        protected float posX;
        public float PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        protected float posY;
        public float PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        protected MyTexture currentTexture;
        public MyTexture CurrentTexture
        {
            get { return currentTexture; }
            set { currentTexture = value; }
        }

        protected int animationFrame;
        public int AnimationFrame
        {
            get { return animationFrame; }
            set { animationFrame = value; }
        }
        
        public Entity(float posX, float posY)
        {
            this.posX = posX;
            this.posY = posY;
            this.animationFrame = 0;
        }

        private bool marketToDelete;
        public bool MarketToDelete
        {
            get { return marketToDelete; }
        }

        public virtual void Update()
        {
           if(posX > BORDER || posY > BORDER || posY < MBORDER || PosX < MBORDER)
           {
               marketToDelete = true;
           }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Czy powinien kontynuowac rysowanie?</returns>
        public virtual bool PreDraw()
        {
            if (currentTexture == null)
            {
                return false;
            }
            if (currentTexture.FramesCount > 0)
            {
                animationFrame++;
                if (animationFrame >= currentTexture.FramesCount * currentTexture.AnimationSpeed)
                {
                    animationFrame = 0;
                }
            }
            return true;
        }

        public virtual void Draw()
        {
            if(!PreDraw())
            {
                return;
            }
            ActualDraw();  
        }

        public virtual void ActualDraw()
        {
            //TODO Komenty!!
            GameMain.SpriteBatch.Draw(currentTexture.Texture, new Vector2((int)(posX * 64), (int)(posY * 64)), GetCurrentSourceRectangle(), Color.White, 0, new Vector2(), 2.0f, SpriteEffects.None, currentTexture.DepthOfDrawing+PosY/1000);
        }

        public virtual Rectangle GetCurrentSourceRectangle()
        {
            return currentTexture.GetCurrentSourceRectangle(animationFrame);
        }
    }
}
