using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Entities
{
    public class EntityLiving : EntityMovable
    {
        protected double shootingRotation;
        public double ShootingRotation
        {
            get { return shootingRotation; }
        }

        private int maxHp;
        public int MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }

        protected int LastHp;
        public int LastHp1
        {
            get { return LastHp; }
        }

        protected int currentHp;
        public int CurrentHp
        {
            get { return currentHp; }
            set { currentHp = value;  }
        }

        protected int shootingSpeed;
        /// <summary>
        /// Odstęp pomiędzy strzałami w tickach
        /// </summary>
        public int ShootingSpeed
        {
            get { return shootingSpeed; }
            set { shootingSpeed = value; }
        }

        /// <summary>
        /// Pozaostały czas przed możliwością ponownego strzelenia
        /// </summary>
        protected int timeLeftBeforeNextShot;

        protected bool isShooting;
        public bool IsShooting
        {
            get { return isShooting; }
        }

        public EntityLiving(float posX, float posY)
            : base(posX, posY)
        {
            timeLeftBeforeNextShot = 0;
        }

        public override void Update()
        {
            //else if(currentColor)
            LastHp = currentHp;
            if (timeLeftBeforeNextShot != 0)
            {
                timeLeftBeforeNextShot--;
            }
            if(isShooting && timeLeftBeforeNextShot == 0)
            {
                timeLeftBeforeNextShot = shootingSpeed;
                EntityBullet bullet = new EntityBullet(this.PosX + 0.25f, this.PosY + 0.25f);
                bullet.CurrentVelocity = 0.1f;
                bullet.Rotation = shootingRotation;
                GameMain.CurrentWorld.AddEntity(bullet);
            }
            if (CurrentHp <= 0)
            {
                MarkedToDelete = true;
                GameMain.CurrentPlayer.Score++;
            }
            base.Update();
        }
    }
}
