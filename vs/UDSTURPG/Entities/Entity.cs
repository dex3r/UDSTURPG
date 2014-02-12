using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;
using RPG.Main;
using RPG.Utils;
using RPG.Controls;
using RPG.Rendering;
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

        protected bool marketToDelete;
        public bool MarketToDelete
        {
            get { return marketToDelete; }
            set { marketToDelete = value; }
        }

        private uint id;
        public uint Id
        {
            get { return id; }
        }

        private bool isColidable;
        public bool IsColidable
        {
            get { return isColidable; }
            set { isColidable = value; }
        }

        private float collisionBoxX;

        public float CollisionBoxX
        {
            get { return collisionBoxX; }
            set { collisionBoxX = value;  }
        }
        private float collisionBoxY;

        public float CollisionBoxY
        {
            get { return collisionBoxY; }
            set { collisionBoxY = value;  }
        }
        private float collisionBoxWidth;

        public float CollisionBoxWidth
        {
            get { return collisionBoxWidth; }
            set { collisionBoxWidth = value; }
        }
        private float collisionBoxHeight;

        public float CollisionBoxHeight
        {
            get { return collisionBoxHeight; }
            set { collisionBoxHeight = value; }
        }

        public Entity(float posX, float posY)
        {
            this.posX = posX;
            this.posY = posY;
            this.animationFrame = 0;
            this.id = GameMain.EntitiesId++;
        }

        public Entity SetCollisionBox(float x, float y, float width, float height)
        {
            this.collisionBoxX = x;
            this.collisionBoxY = y;
            this.collisionBoxWidth = width;
            this.collisionBoxHeight = height;
            return this;
        }

        public virtual void Update()
        {
            if (posX > BORDER || posY > BORDER || posY < MBORDER || PosX < MBORDER)
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
            if (!PreDraw())
            {
                return;
            }
            ActualDraw();
        }

        public virtual void ActualDraw()
        {
            //TODO Komenty!!
            //GameMain.SpriteBatch.Draw(currentTexture.Texture, new Vector2((int)(posX * 64), (int)(posY * 64)), GetCurrentSourceRectangle(), Color.White, 0, new Vector2(), 1.0f, SpriteEffects.None, currentTexture.DepthOfDrawing + PosY / 1000);
            GlobalRenderer.DrawEntity(currentTexture.Texture, posX, posY, GetCurrentSourceRectangle(), currentTexture.DepthOfDrawing + PosY / 1000);
            if (MyKeyboard.KeyF10.IsPressed)
            {
                GameMain.SpriteBatch.DrawRectangle(new Vector2((PosX + CollisionBoxX) * 64, (PosY + CollisionBoxY) * 64), new Vector2((CollisionBoxX + collisionBoxWidth) * 32, (CollisionBoxY + CollisionBoxHeight) * 32), Color.Red, 3.0f);
            }

        }

        public virtual Rectangle GetCurrentSourceRectangle()
        {
            return currentTexture.GetCurrentSourceRectangle(animationFrame);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Zwraca tylko odległość od x y do x y</returns>
        public virtual double Distance(Entity a)
        {
            return Math.Sqrt(Math.Pow(a.PosX - PosX, 2) + Math.Pow(a.PosY - PosY, 2));
        }

        public virtual bool Collision(Entity a)
        {
            if (Distance(a) < 2)
            {
                if (PosX + CollisionBoxX + CollisionBoxWidth < a.PosX + a.CollisionBoxX) return false;
                if (a.PosX + a.CollisionBoxX + a.CollisionBoxWidth < PosX + CollisionBoxX) return false;
                if (PosY + CollisionBoxY + CollisionBoxHeight < a.PosY + a.CollisionBoxY) return false;
                if (a.PosY + a.CollisionBoxY + a.CollisionBoxHeight < PosY + CollisionBoxY) return false;
                return true;
            }
            return false;
        }
    }
}
