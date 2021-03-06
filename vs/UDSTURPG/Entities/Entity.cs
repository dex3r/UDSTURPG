﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RPG.Textures2D;
using RPG.Main;
using RPG.Utils;
using RPG.Controls;
using RPG.Rendering;

namespace RPG.Entities
{
    public class Entity
    {
        protected static readonly Random random = new Random();

        // Granica, za którą obiekt znika
        public const int BORDER = 100;
        public const int MBORDER = -BORDER;

        protected float posX;
        protected float posY;
        protected MyTexture currentTexture;
        protected int animationFrame;
        protected bool markedToDelete;
        protected bool markedToDeleteInNextTick;
        private uint id;
        private bool isColidable;
        private float collisionBoxX;
        private float collisionBoxY;
        private float collisionBoxWidth;
        private float collisionBoxHeight;
        protected Color currentColor;


        //!? Properties region
        #region PROPERTIES
        public float PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        public float PosY
        {
            get { return posY; }
            set { posY = value; }
        }
        public MyTexture CurrentTexture
        {
            get { return currentTexture; }
            set { currentTexture = value; }
        }
        public int AnimationFrame
        {
            get { return animationFrame; }
            set { animationFrame = value; }
        }
        public bool MarkedToDelete
        {
            get { return markedToDelete; }
            set { markedToDelete = value; }
        }
        public bool MarkedToDeleteInNextTick
        {
            get { return markedToDeleteInNextTick; }
            set { markedToDeleteInNextTick = value; }
        }
        public uint Id
        {
            get { return id; }
        }
        public bool IsColidable
        {
            get { return isColidable; }
            set { isColidable = value; }
        }
        public float CollisionBoxX
        {
            get { return collisionBoxX; }
            set { collisionBoxX = value; }
        }
        public float CollisionBoxY
        {
            get { return collisionBoxY; }
            set { collisionBoxY = value; }
        }
        public float CollisionBoxWidth
        {
            get { return collisionBoxWidth; }
            set { collisionBoxWidth = value; }
        }
        public float CollisionBoxHeight
        {
            get { return collisionBoxHeight; }
            set { collisionBoxHeight = value; }
        }
        public Color CurrentColor
        {
            get { return currentColor; }
        }
        #endregion
        //!? END of properties region

        public Entity(float posX, float posY)
        {
            this.posX = posX;
            this.posY = posY;
            this.animationFrame = 0;
            this.id = GameMain.EntitiesId++;
            currentColor = new Color(1.0f, 1.0f, 1.0f);
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
                markedToDelete = true;
                return;
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
            GlobalRenderer.DrawEntity(currentTexture.Texture, posX, posY, GetCurrentSourceRectangle(), currentTexture.DepthOfDrawing + PosY / 1000, currentColor);
            if (GlobalRenderer.ShouldRenderHitobxes)
            {
                //GameMain.SpriteBatch.DrawRectangle(new Vector2((PosX + CollisionBoxX) * 64, (PosY + CollisionBoxY) * 64), new Vector2((CollisionBoxX + collisionBoxWidth) * 64.0f, (CollisionBoxY + CollisionBoxHeight) * 64.0f), Color.Red, 3.0f);
                GameMain.SpriteBatch.DrawRectangle(new Vector2((PosX + CollisionBoxX) * 64, (PosY + CollisionBoxY) * 64), new Vector2((collisionBoxWidth) * 64.0f, (CollisionBoxHeight) * 64.0f), Color.Red, 3.0f);
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
        public virtual double Distance(Entity entity)
        {
            return Math.Sqrt(Math.Pow(entity.posX - posX + ((entity.collisionBoxWidth - collisionBoxWidth) / 2), 2) + Math.Pow(entity.posY - posY + ((entity.collisionBoxWidth - collisionBoxWidth) / 2), 2));
        }

        public virtual bool Collision(Entity entity, double distance = 2)
        {
            //TODO: zamienić "PosX + CollisionBoxX" na zmienne klasy, aktualizowane co tick
                if (PosX + CollisionBoxX + CollisionBoxWidth < entity.PosX + entity.CollisionBoxX) return false;
                if (PosY + CollisionBoxY + CollisionBoxHeight < entity.PosY + entity.CollisionBoxY) return false;
                if (entity.PosX + entity.CollisionBoxX + entity.CollisionBoxWidth < PosX + CollisionBoxX) return false;
                if (entity.PosY + entity.CollisionBoxY + entity.CollisionBoxHeight < PosY + CollisionBoxY) return false;
                return true;
        }
    }
}
