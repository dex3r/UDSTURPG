using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Entities
{
    public class Entity
    {
        float posX;
        public float PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        float posY;
        public float PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        private float prevX;
        public float PrevX
        {
            get { return prevX; }
            set { prevX = value; }
        }
        private float prevY;
        public float PrevY
        {
            get { return prevY; }
            set { prevY = value; }
        }

        public virtual Texture2D Texture
        {
            get
            {
                return null;
            }
        }
        
        public Entity()
        {

        }

        public virtual void Update()
        {
            prevX = posX;
            prevY = posY;
        }
    }
}
