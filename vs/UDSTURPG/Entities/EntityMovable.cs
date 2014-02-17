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

        public bool BoundryCollision(bool top = true, bool left = true, bool bottom = true, bool right = true)
        {
            if (left && posX <= 0) { posX = 0; return true; }
            if (right && posX >= Chunk.CHUNK_SIZE_X) { posX = Chunk.CHUNK_SIZE_X; return true; }
            if (top && posY <= 0) { posY = 0; return true; }
            if (bottom && posY >= Chunk.CHUNK_SIZE_Y) { posY = Chunk.CHUNK_SIZE_Y; return true; }
            return false;
        }
    }
}
