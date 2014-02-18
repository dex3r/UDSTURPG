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
            base.Update();
            Entity target = null;
            double lastDistance = 0.0f;
            double temp;
            foreach (Entity en in GameMain.CurrentWorld.Entities)
            {
                if (en is EntityMob)
                {
                    temp = en.Distance(en);
                    if (temp < 5)
                    {
                        lastDistance = temp;
                        target = en;
                    }
                }
            }
            if(target != null)
            {
                EntityBullet bullet = new EntityBullet(this.PosX + 0.25f, this.PosY + 0.25f,10);
                bullet.CurrentVelocity = 0.1f;
                Vector2 interp = Vector2.Subtract(new Vector2(this.PosX + 0.45f, this.posY + 0.45f), new Vector2(target.PosX, target.PosY));
                interp.Normalize();
                interp = Vector2.Multiply(interp, (float)Math.PI);
                bullet.Rotation = Math.Atan2(-interp.Y, -interp.X);
                rotation = bullet.Rotation;
                GameMain.CurrentWorld.AddEntity(bullet);
            }
        }
    }
}
