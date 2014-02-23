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
        private float prevY;
        protected float currentVelocity;
        private float acceleration = 0.004f;
        protected float maxSpeed;
        protected double rotation;
        private float hitRecoil;
        private bool isAnimatedOnlyOnMove;

        //!? Properties region
        #region PROPERTIES
        public float PrevX
        {
            get { return prevX; }
            set { prevX = value; }
        }
        public float PrevY
        {
            get { return prevY; }
            set { prevY = value; }
        }
        public float CurrentVelocity
        {
            get { return currentVelocity; }
            set { currentVelocity = value; }
        }
        public float Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }
        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }
        /// <summary>
        /// Rotation in radians
        /// </summary>
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        /// <summary>
        /// Odrzut po oberwaniu strzału
        /// </summary>
        public float HitRecoil
        {
            get { return hitRecoil; }
            set { hitRecoil = value; }
        }
        public bool IsAnimatedOnlyOnMove
        {
            get { return isAnimatedOnlyOnMove; }
            set { isAnimatedOnlyOnMove = value; }
        }
        #endregion
        //!? END of properties region

        public EntityMovable(float posX, float posY)
            : base(posX, posY)
        {
            isAnimatedOnlyOnMove = true;
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
            if (HitRecoil > 0)
            {
                currentVelocity = -HitRecoil;
                HitRecoil -= 0.05f+acceleration;
                if(acceleration <= 0.03f)
                acceleration *= 2;
                if (HitRecoil <= 0)
                {
                    acceleration = 0.004f;
                    HitRecoil = 0;
                    currentVelocity = 0;
                    rotation = Math.PI;
                }
            }
        }

        public override bool PreDraw()
        {
            bool flag = base.PreDraw();
            if (isAnimatedOnlyOnMove && currentVelocity == 0)
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
