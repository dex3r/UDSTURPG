using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Entities;

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

        //protected float currentVelocityY;
        //public float CurrentVelocityY
        //{
        //    get { return currentVelocityY; }
        //    set { currentVelocityY = value; }
        //}

        //protected float currentVelocityX;
        //public float CurrentVelocityX
        //{
        //    get { return currentVelocityX; }
        //    set { currentVelocityX = value; }
        //}

        protected float currentVelocity;
        public float CurrentVelocity
        {
            get { return currentVelocity; }
            set { currentVelocity = value; }
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

        public EntityMovable(float posX, float posY) : base(posX, posY)
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
    }
}
