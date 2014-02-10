using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Entities;

namespace RPG.Entities
{
    public class Movable : Entity
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
        public virtual void Update()
        {
            prevX = PosX;
            prevY = PosY;
        }
    }
}
