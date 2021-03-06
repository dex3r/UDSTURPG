﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RPG.Textures2D;
using RPG.Main;
using RPG.Controls;
using RPG.Rendering;
using RPG.Utils;

namespace RPG.Entities
{
    public class EntityMob : EntityLiving
    {
        private EnumSheetNormalMob movementTextureState;
        private MobType mobType;
        private int worth;
        private int stepLength;

        //!? Properties region
        #region PROPERTIES
        public EnumSheetNormalMob MovementTextureState
        {
            get { return movementTextureState; }
            set { movementTextureState = value; }
        }
        public MobType MobType
        {
            get { return mobType; }
        }
        public int Worth
        {
            get { return worth; }
        }
        /// <summary>
        /// Długość kroku (w tickach)
        /// </summary>
        public int StepLength
        {
            get { return stepLength; }
        }
        #endregion
        //!? END of properties region

        public EntityMob(float posX, float posY, MobType mobType)
            : base(posX, posY)
        {
            this.mobType = mobType;
            this.CurrentHp = this.MaxHp = mobType.Hp;
            this.maxSpeed = mobType.Speed;
            this.damage = mobType.BaseDmg;
            this.currentTexture = mobType.Texture;
            this.worth = mobType.Worth;
            SetCollisionBox(mobType.CollisionBoxX, mobType.CollisionBoxY, mobType.CollisionBoxWidth, mobType.CollisionBoxHeight);
            movementTextureState = EnumSheetNormalMob.Down;
            currentVelocity = maxSpeed;
            rotation = Math.PI;
        }

        public override void Update()
        {
            BoundryCollision(true, false, true, false);
            if (currentHp <= 0)
            {
                GameMain.CurrentPlayer.Score += worth;
                EntityCoin.Loot(posX + (CollisionBoxX + CollisionBoxWidth) / 2, posY + (CollisionBoxY + CollisionBoxHeight) / 2, this.worth * 10);
            }
            base.Update();
            if (this.mobType.WalkingStyle == EnumMobWalkingStyle.Stuttery)
            {
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
            }
            else if (currentVelocity == 0)
            {
                currentVelocity = maxSpeed;
            }
        }

        public override bool PreDraw()
        {
            bool flag = base.PreDraw();
            if (MobType == MobType.MobBat)
            {
                GlobalRenderer.DrawEntity(MyTexture.MobBatShadow.Texture, PosX, PosY + 0.5f, MyTexture.MobBatShadow.SourceRectangle, MyTexture.MobBatShadow.DepthOfDrawing, new Color(Color.White, 0.3f));
            }
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
            }
            if (GlobalRenderer.ShouldRenderHitobxes)
            {
                GameMain.SpriteBatch.DrawCircle(new Vector2((PosX + CollisionBoxX + CollisionBoxWidth / 2) * 64, (PosY + CollisionBoxY + CollisionBoxHeight / 2) * 64), 3, 10, Color.Red);
            }
            return flag;
        }

        public override Rectangle GetCurrentSourceRectangle()
        {
            return currentTexture.GetCurrentSourceRectangle(animationFrame, (int)MovementTextureState);
        }
    }
}
