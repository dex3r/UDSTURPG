using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Entities;
using RPG.Worlds;

namespace RPG.Entities
{
    public class EntityMovable : Entity
    {
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

        protected float currentVelocity;
        public float CurrentVelocity
        {
            get { return currentVelocity; }
            set { currentVelocity = value; }
        }

        protected float maxSpeed;

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        protected double rotation;
        /// <summary>
        /// Rotation in radians
        /// </summary>
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private float hitRecoil;
        /// <summary>
        /// Odrzut po oberwaniu strzału
        /// </summary>
        public float HitRecoil
        {
            get { return hitRecoil; }
            set { hitRecoil = value; }
        }

        public EntityMovable(float posX, float posY)
            : base(posX, posY)
        {

        }

        public override void Update()
        {
            base.Update();
            prevX = PosX;
            prevY = PosY;
            float rotX = (float)Math.Cos(rotation);
            float rotY = (float)Math.Sin(rotation);
            posX += rotX * currentVelocity;
            posY += rotY * currentVelocity;
        }

        public override bool PreDraw()
        {
            bool flag = base.PreDraw();
            if (currentVelocity == 0)
            {
                animationFrame = 0;
            }
            return flag;
        }

        public void BoundryCollision(bool top = true, bool left = true, bool bottom = true, bool right = true)
        {
            if (left && posX <= 0) { posX = 0;}
            if (right && posX >= Chunk.CHUNK_SIZE_X - CollisionBoxWidth) { posX = Chunk.CHUNK_SIZE_X - CollisionBoxWidth; }
            if (top && posY <= 0) { posY = 0; }
            if (bottom && posY >= Chunk.CHUNK_SIZE_Y - CollisionBoxHeight) { posY = Chunk.CHUNK_SIZE_Y - CollisionBoxHeight; }
        }
    }
}
