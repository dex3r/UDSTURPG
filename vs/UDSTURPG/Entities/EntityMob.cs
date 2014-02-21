﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;
using RPG.Main;
using RPG.Controls;
using RPG.Rendering;
using RPG.Utils;
using Microsoft.Xna.Framework;

namespace RPG.Entities
{
    public class EntityMob : EntityLiving
    {
        private int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        private EnumSheetNormalMob movementTextureState;
        public EnumSheetNormalMob MovementTextureState
        {
            get { return movementTextureState; }
            set { movementTextureState = value; }
        }

        private MobType mobType;
        public MobType MobType
        {
            get { return mobType; }
        }

        private int stepLength;
        /// <summary>
        /// Długość kroku (w tickach)
        /// </summary>
        public int StepLength
        {
            get { return stepLength; }
        }

        public EntityMob(float posX, float posY, MobType mobType)
            : base(posX, posY)
        {
            this.mobType = mobType;
            this.CurrentHp = this.MaxHp = mobType.Hp;
            this.maxSpeed = mobType.Speed;
            this.damage = mobType.BaseDmg;
            this.currentTexture = mobType.Texture;
            SetCollisionBox(mobType.CollisionBoxX, mobType.CollisionBoxY, mobType.CollisionBoxWidth, mobType.CollisionBoxHeight);
            movementTextureState = EnumSheetNormalMob.Down;
            currentVelocity = maxSpeed;
            rotation = Math.PI;
            this.IsColidable = true;
        }

        public override void Update()
        {
            BoundryCollision(true, false, true, false);
            if (currentHp < LastHp)
            {
                //TODO: podrasować kolor
                currentColor = new Color(0.6f, 0.2f, 0.2f);
            }
            else if(currentColor.G != 255)
            {
                currentColor = new Color(currentColor.ToVector3() + new Vector3(0.08f, 0.08f, 0.08f));
            }
            base.Update();
            if (mobType.StepInterval != 0)
            {
                stepLength++;
                if (currentVelocity > 0 && stepLength >= mobType.StepLength)
                {
                    stepLength = 0;
                    currentVelocity = 0;
                }
                else if (currentVelocity == 0 && stepLength >= mobType.StepInterval)
                {
                    stepLength = 0;
                    currentVelocity = maxSpeed;
                }
            }
            if(HitRecoil > 0)
            {
                currentVelocity = -HitRecoil;
                HitRecoil -= 0.1f;
                if (HitRecoil <= 0)
                {
                    HitRecoil = 0;
                    currentVelocity = 0;
                    rotation = Math.PI;
                }
            }
        }

        public override bool PreDraw()
        {
            bool flag = base.PreDraw();
            if (HitRecoil == 0) //Zablokowanie tekstury
            {
                double degreesRotation = 180.0d / Math.PI * -rotation;
                if (rotation > 0)
                {
                    degreesRotation += 360;
                }

                if (degreesRotation <= 45)
                {
                    MovementTextureState = EnumSheetNormalMob.Right;
                }
                else if (degreesRotation <= 135)
                {
                    MovementTextureState = EnumSheetNormalMob.Up;
                }
                else if (degreesRotation <= 215)
                {
                    MovementTextureState = EnumSheetNormalMob.Left;
                }
                else if (degreesRotation <= 315)
                {
                    MovementTextureState = EnumSheetNormalMob.Down;
                }
                else
                {
                    MovementTextureState = EnumSheetNormalMob.Right;
                }
                if (MyKeyboard.KeyF10Pressed)
                {
                    GameMain.SpriteBatch.DrawCircle(new Vector2((PosX + 0.7f)*64, (PosY + 0.7f)*64),3,10,Color.Red);
                }
            }
            return flag;
        }

        public override Rectangle GetCurrentSourceRectangle()
        {
            return currentTexture.GetCurrentSourceRectangle(animationFrame, (int)MovementTextureState);
        }
    }
}
