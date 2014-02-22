using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.Rendering;
using RPG.Textures2D;

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

        protected int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public EntityLiving(float posX, float posY)
            : base(posX, posY)
        {
            timeLeftBeforeNextShot = 0;
            this.maxHp = this.currentHp = 1;
        }

        public override void Update()
        {
            //else if(currentColor)
            if (currentHp < LastHp)
            {
                //TODO: podrasować kolor
                currentColor = new Color(0.6f, 0.2f, 0.2f);
            }
            else if (currentColor.G != 255)
            {
                currentColor = new Color(currentColor.ToVector3() + new Vector3(0.08f, 0.08f, 0.08f));
            }
            LastHp = currentHp;
            if (timeLeftBeforeNextShot != 0)
            {
                timeLeftBeforeNextShot--;
            }
            if(isShooting && timeLeftBeforeNextShot == 0)
            {
                timeLeftBeforeNextShot = shootingSpeed;
                //EntityBullet bullet = new EntityBullet(this.PosX + 0.25f, this.PosY + 0.25f);
                EntityBullet bullet = this.GetNewBullet();
                bullet.CurrentVelocity = 0.1f;
                bullet.Rotation = shootingRotation;
                GameMain.CurrentWorld.AddEntity(bullet);
            }
            if (CurrentHp <= 0)
            {
                GameMain.CurrentWorld.AddEntity(new EntityEffect(this.posX + ((this.CollisionBoxX + this.CollisionBoxWidth) / 2) - (MyTexture.EffectEnityDiePuff.SourceRectangle.Width / 64.0f), this.posY + ((this.CollisionBoxY + this.CollisionBoxHeight) / 2) - (MyTexture.EffectEnityDiePuff.SourceRectangle.Height / 64.0f), MyTexture.EffectEnityDiePuff));

                MarkedToDelete = true;
                GameMain.CurrentPlayer.Score++;
            }
            base.Update();
        }

        public virtual EntityBullet GetNewBullet()
        {
            return new EntityBullet(this.posX + this.CollisionBoxX + this.CollisionBoxWidth, this.posY + this.CollisionBoxY + this.CollisionBoxHeight, damage);
        }
    }
}
