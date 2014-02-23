using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.Textures2D;
using RPG.Main;


namespace RPG.Entities
{
    public class EntityTurret : EntityLiving
    {
        public EntityTurret(float posX, float posY)
            : base(posX, posY)
        {
            currentTexture = MyTexture.Turret;
            SetCollisionBox(0, 0, 0.5f, 0.5f);
            this.ShootingSpeed = 32;
            this.damage = 25;
        }

        public override Rectangle GetCurrentSourceRectangle()
        {
            int frameFromRotation;
            double degreesRotation = 180.0d / Math.PI * -rotation;
            //? ZMIENIĆ NA JAKŚ FUNCKJĘ MATEMATYCZNĄ
            if (rotation > 0)
            {
                degreesRotation = 360 + degreesRotation;
            }

            if (degreesRotation <= 22.5)
            {
                frameFromRotation = 6;
            }
            else if (degreesRotation <= 67.5)
            {
                frameFromRotation = 5;
            }
            else if (degreesRotation <= 112.5)
            {
                frameFromRotation = 4;
            }
            else if (degreesRotation <= 157.5)
            {
                frameFromRotation = 3;
            }
            else if (degreesRotation <= 202.5)
            {
                frameFromRotation = 2;
            }
            else if (degreesRotation <= 247.5)
            {
                frameFromRotation = 1;
            }
            else if (degreesRotation <= 292.5)
            {
                frameFromRotation = 0;
            }
            else if (degreesRotation <= 340.5)
            {
                frameFromRotation = 7;
            }
            else
            {
                frameFromRotation = 6;
            }
            Rectangle rect = currentTexture.GetCurrentSourceRectangle(frameFromRotation, 0, 1);
            return rect;
        }

        public override void Update()
        {
            if (timeLeftBeforeNextShot <= 1)
            {
                Entity target = null;
                double lastDistance = double.MaxValue;
                double distance;
                foreach (Entity en in GameMain.CurrentWorld.Entities)
                {
                    if (en is EntityMob)
                    {
                        distance = this.Distance(en);
                        if (distance < 10 && lastDistance > distance)
                        {
                            lastDistance = distance;
                            target = en;
                            if(Collision(en))
                            {
                                EntityMob mob = (EntityMob)en;
                                HitRecoil = 0.5f;

                                Vector2 interp = Vector2.Subtract(new Vector2((mob.PosX + mob.CollisionBoxX + mob.CollisionBoxWidth / 2) * 64, (mob.PosY + mob.CollisionBoxY + mob.CollisionBoxHeight / 2) * 64), new Vector2((PosX + 0.5f) * 64, (PosY + 0.5f) * 64));
                                interp.Normalize();
                                interp = Vector2.Multiply(interp, (float)Math.PI);
                                Rotation = Math.Atan2(interp.Y, interp.X);
                            }
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 interp = Vector2.Subtract(new Vector2(this.PosX + ((this.CollisionBoxX + this.CollisionBoxWidth) * 2.0f), this.posY + ((this.CollisionBoxY + this.CollisionBoxHeight) * 2.0f)), new Vector2(target.PosX + target.CollisionBoxX + target.CollisionBoxWidth, target.PosY + target.CollisionBoxY + target.CollisionBoxHeight));
                    interp.Normalize();
                    interp = Vector2.Multiply(interp, (float)Math.PI);
                    rotation = shootingRotation = Math.Atan2(-interp.Y, -interp.X);
                    isShooting = true;
                }
            }
            base.Update();
        }
    }
}
