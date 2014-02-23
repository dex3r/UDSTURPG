using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.Rendering;
using RPG.Textures2D;
using RPG.Utils;
using RPG.Main;

namespace RPG.Entities
{
    public class EntityLiving : EntityMovable
    {
        protected double shootingRotation;
        private int maxHp;
        protected int lastHp;
        protected int currentHp;
        private Color healthColor = Color.Green;
        protected int shootingSpeed;
        /// <summary>
        /// Pozaostały czas przed możliwością ponownego strzelenia
        /// </summary>
        protected int timeLeftBeforeNextShot;
        protected bool isShooting;
        protected int damage;
        protected int barFrame;
        private bool barDisplay;

        //!? Properties region
        #region PROPERTIES
        public double ShootingRotation
        {
            get { return shootingRotation; }
        }
        public int MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int LastHp
        {
            get { return lastHp; }
        }
        public int CurrentHp
        {
            get { return currentHp; }
            set { currentHp = value; }
        }
        public Color HealthColor
        {
            get { return healthColor; }
        }
        /// <summary>
        /// Odstęp pomiędzy strzałami w tickach
        /// </summary>
        public int ShootingSpeed
        {
            get { return shootingSpeed; }
            set { shootingSpeed = value; }
        }
        public bool IsShooting
        {
            get { return isShooting; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int BarFrame
        {
            get { return barFrame; }
            set { barFrame = value; }
        }
        #endregion
        //!? END of properties region

        public EntityLiving(float posX, float posY)
            : base(posX, posY)
        {
            this.barDisplay = false;
            timeLeftBeforeNextShot = 0;
            this.maxHp = this.currentHp = 1;
        }

        public override void Update()
        {
            //else if(currentColor)
            if (currentHp < lastHp)
            {
                //TODO: podrasować kolor
                currentColor = new Color(0.6f, 0.2f, 0.2f);
            }
            else if (currentColor.G != 255)
            {
                currentColor = new Color(currentColor.ToVector3() + new Vector3(0.08f, 0.08f, 0.08f));
            }
            lastHp = currentHp;
            if (timeLeftBeforeNextShot != 0)
            {
                timeLeftBeforeNextShot--;
            }
            if (isShooting && timeLeftBeforeNextShot == 0)
            {
                timeLeftBeforeNextShot = shootingSpeed;
                //EntityBullet bullet = new EntityBullet(this.PosX + 0.25f, this.PosY + 0.25f);
                EntityBullet bullet = this.GetNewBullet();
                bullet.CurrentVelocity = 0.1f;
                bullet.Rotation = shootingRotation;
                GameMain.CurrentWorld.AddEntity(bullet);
            }
            if (CurrentHp != MaxHp)
            {
                barDisplay = true;
                if (CurrentHp <= 0)
                {
                    GameMain.CurrentWorld.AddEntity(new EntityEffect(this.posX + ((this.CollisionBoxX + this.CollisionBoxWidth) / 2) - (MyTexture.EffectEnityDiePuff.SourceRectangle.Width / 64.0f), this.posY + ((this.CollisionBoxY + this.CollisionBoxHeight) / 2) - (MyTexture.EffectEnityDiePuff.SourceRectangle.Height / 64.0f), MyTexture.EffectEnityDiePuff));

                    MarkedToDelete = true;
                }
                else
                {
                    BarFrame = (int)(((float)CurrentHp / (float)MaxHp) * 21);
                    if (BarFrame < 8)
                    {
                        healthColor = Color.Red;
                    }
                    if (BarFrame >= 8 && BarFrame < 15)
                    {
                        healthColor = Color.Orange;
                    }
                    if (BarFrame >= 15)
                    {
                        healthColor = Color.Green;
                    }
                    //healthColor = ColorConversion.HSVtoRGB((BarFrame/21f*80)/240,1f,0.5f,1f);
                }
            }
            base.Update();
        }

        public override void ActualDraw()
        {
            base.ActualDraw();
            if (barDisplay == true)
            {
                GlobalRenderer.DrawEntity(MyTexture.HealthBar.Texture, PosX + ((CollisionBoxX + CollisionBoxWidth) / 2) - 0.5f, PosY + CollisionBoxY + CollisionBoxHeight, MyTexture.HealthBar.GetCurrentSourceRectangle(BarFrame), MyTexture.HealthBar.DepthOfDrawing, healthColor);
                GlobalRenderer.DrawEntity(MyTexture.HealthBarOutline.Texture, PosX + ((CollisionBoxX + CollisionBoxWidth) / 2) - 0.5f, PosY + CollisionBoxY + CollisionBoxHeight, MyTexture.HealthBar.GetCurrentSourceRectangle(BarFrame), MyTexture.HealthBarOutline.DepthOfDrawing, Color.White);
                GlobalRenderer.DrawEntity(MyTexture.HealthBarUnderlay.Texture, PosX + ((CollisionBoxX + CollisionBoxWidth) / 2) - 0.5f, PosY + CollisionBoxY + CollisionBoxHeight, MyTexture.HealthBar.GetCurrentSourceRectangle(BarFrame), MyTexture.HealthBarUnderlay.DepthOfDrawing, Color.White);
            }
        }

        public virtual EntityBullet GetNewBullet()
        {
            return new EntityBullet(this.posX + this.CollisionBoxX + this.CollisionBoxWidth, this.posY + this.CollisionBoxY + this.CollisionBoxHeight, damage);
        }
    }
}
