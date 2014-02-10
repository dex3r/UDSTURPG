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

        public virtual void Update()
        {
           
        }

        public virtual void Draw()
        {
            if(currentTexture == null)
            {
                return;
            }
            if(currentTexture.FramesCount > 0)
            {
                animationFrame++;
                if(animationFrame >= currentTexture.FramesCount * currentTexture.AnimationSpeed)
                {
                    animationFrame = 0;
                }
            }
        }
    }
}
