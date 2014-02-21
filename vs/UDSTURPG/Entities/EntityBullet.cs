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
    public class EntityBullet : EntityMovable
    {
        private int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public EntityBullet(float posX, float posY, int damage = 15)
            : base(posX, posY)
        {
            currentTexture = MyTexture.Bullet;
            SetCollisionBox(0, 0, 0.5f, 0.5f);
            Damage = damage;
        }

        public override void Update()
        {
           
            foreach(Entity en in GameMain.CurrentWorld.Entities)
            {
                if (en is EntityMob)
                {
                    if (!en.MarkedToDelete && Collision(en))
                    {
                        EntityMob mob = (EntityMob)en;
                        mob.HitRecoil = 0.2f;
                        mob.CurrentHp -= Damage;

                        Vector2 interp = Vector2.Subtract(new Vector2((PosX+0.25f) * 64, (PosY+0.25f) * 64), new Vector2((mob.PosX + 0.7f)*64, (mob.PosY + 0.7f)*64));
                        interp.Normalize();
                        interp = Vector2.Multiply(interp, (float)Math.PI);
                        mob.Rotation = Math.Atan2(interp.Y, interp.X);

                        if(mob.CurrentHp <= 0)
                        {
                            mob.MarkedToDelete = true;
                            GameMain.CurrentPlayer.Score++;
                        }
                        markedToDelete = true;
                    }
                }
            }
            base.Update();
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
    }
}
