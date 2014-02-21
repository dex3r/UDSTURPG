using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;
using RPG.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RPG.Entities
{
    public class EntityTurret : EntityLiving
    {
        public EntityTurret(float posX, float posY)
            : base(posX, posY)
        {
            currentTexture = MyTexture.Turret;
            SetCollisionBox(0, 0, 0.5f, 0.5f);
            this.ShootingSpeed = 35;
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
                        if (distance < 15 && lastDistance > distance)
                        {
                            lastDistance = distance;
                            target = en;
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
