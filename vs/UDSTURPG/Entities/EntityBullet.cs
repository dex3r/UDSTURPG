﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.Textures2D;
using RPG.Main;

namespace RPG.Entities
{
    public class EntityBullet : EntityMovable
    {
        private int damage;

        //!? Properties region
        #region PROPERTIES
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        #endregion
        //!? END of properties region

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
                        mob.HitRecoil = 0.1f;
                        mob.CurrentHp -= Damage;

                        Vector2 interp = Vector2.Subtract(new Vector2((PosX + 0.25f) * 64, (PosY + 0.25f) * 64), new Vector2((mob.PosX + mob.CollisionBoxX + mob.CollisionBoxWidth / 2) * 64, (mob.PosY + mob.CollisionBoxY + mob.CollisionBoxHeight / 2) * 64));
                        interp.Normalize();
                        interp = Vector2.Multiply(interp, (float)Math.PI);
                        mob.Rotation = Math.Atan2(interp.Y, interp.X);

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
