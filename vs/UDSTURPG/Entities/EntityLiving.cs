using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG.Entities
{
    public class EntityLiving : EntityMovable
    {
        private int maxHp;
        public int MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }

        private int currentHp;
        public int CurrentHp
        {
            get { return currentHp; }
            set { currentHp = value; }
        }

        public EntityLiving(float posX, float posY)
            : base(posX, posY)
        {

        }
    }
}
