using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RPG.Controls;
using RPG.Textures2D;
using RPG.Main;
using RPG.Rendering;

namespace RPG.Entities
{
    public class EntityPlayer : EntityLiving
    {
        private float playerAcceleration;
        private int invincibleTimer = 0;
        private int invincibleTime = 30;
        private EnumSheetPlayer movementTextureState;
        private int score;
        private int money;

        //!? Properties region
        #region PROPERTIES
        public int InvincibleTime
        {
            get { return invincibleTime; }
        }
        public EnumSheetPlayer MovementTextureState
        {
            get { return movementTextureState; }
            set { movementTextureState = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public int Money
        {
            get { return money; }
            set { money = value; }
        }
        #endregion
        //!? END of properties region

        public EntityPlayer(float posX, float posY)
            : base(posX, posY)
        {
            CurrentTexture = MyTexture.PlayerLordLard;
            maxSpeed = 0.05f;
            playerAcceleration = 0.03f;
            SetCollisionBox(0.2f, 0, 0.6f, 1.0f);
            MaxHp = 100;
            CurrentHp = MaxHp;
            this.damage = 15;
        }

        public override void Update()
        {
            BoundryCollision();
            if (HitRecoil <= 0)
            {
                double y = 0;
                double x = 0;
                if (MyKeyboard.KeyMoveDown.IsPressed)
                {
                    y += Math.PI;
                }
                if (MyKeyboard.KeyMoveUp.IsPressed)
                {
                    y -= Math.PI;
                }
                if (MyKeyboard.KeyMoveLeft.IsPressed)
                {
                    x -= Math.PI;
                }
                if (MyKeyboard.KeyMoveRight.IsPressed)
                {
                    x += Math.PI;
                }
                if (x != 0 || y != 0)
                {
                    rotation = Math.Atan2(y, x);
                    currentVelocity += playerAcceleration;
                    //Camera.X = posX * 64 - GameMain.graphicsDeviceManager.PreferredBackBufferWidth / 2+32;
                    //Camera.Y = posY * 64 - GameMain.graphicsDeviceManager.PreferredBackBufferHeight / 2+32;
                }
                else
                {
                    if (currentVelocity > 0)
                    {
                        currentVelocity -= playerAcceleration;
                        if (currentVelocity < 0)
                        {
                            currentVelocity = 0;
                        }
                    }
                }
                if (currentVelocity > maxSpeed)
                {
                    currentVelocity = maxSpeed;
                }
                else if (currentVelocity < 0)
                {
                    currentVelocity = 0;
                }
                if (invincibleTimer == 0)
                {
                    foreach (Entity en in GameMain.CurrentWorld.Entities)
                    {
                        if (en is EntityMob)
                        {
                            if (Collision(en))
                            {
                                EntityMob mob = (EntityMob)en;
                                HitRecoil = 0.25f;

                                Vector2 interpPlayer = Vector2.Subtract(new Vector2((mob.PosX + mob.CollisionBoxX + mob.CollisionBoxWidth / 2) * 64, (mob.PosY + mob.CollisionBoxY + mob.CollisionBoxHeight / 2) * 64), new Vector2((PosX + 0.5f) * 64, (PosY + 0.5f) * 64));
                                interpPlayer.Normalize();
                                interpPlayer = Vector2.Multiply(interpPlayer, (float)Math.PI);
                                Rotation = Math.Atan2(interpPlayer.Y, interpPlayer.X);

                                GameMain.CurrentPlayer.CurrentHp -= 2;
                                invincibleTimer += InvincibleTime;
                            }
                        }
                        if(en is EntityCoin)
                        {
                            EntityCoin coin = (EntityCoin)en;
                            if(Distance(coin) < 4.5)
                            {
                                coin.PullMoney = true;
                            }
                            else
                            {
                                coin.PullMoney = false;
                            }
                            if(Collision(coin))
                            {
                                Money += coin.CoinValue;
                                coin.MarkedToDelete = true;
                            }
                        }
                    }
                    
                }
                else
                {
                    invincibleTimer--;
                }
            }
            base.Update();
            if (MyKeyboard.KeyShoot.IsToggled)
            {
                //TODO Komenty!!
                EntityBullet bullet = new EntityBullet(GameMain.CurrentPlayer.PosX + 0.25f, GameMain.CurrentPlayer.PosY + 0.25f, damage);
                bullet.CurrentVelocity = 0.1f;
                Vector2 interp = Vector2.Subtract(new Vector2((GameMain.CurrentPlayer.PosX + 0.45f) * 64, (GameMain.CurrentPlayer.PosY + 0.45f) * 64), new Vector2(MyMouse.PositionRelativeX, MyMouse.PositionRelativeY));
                interp.Normalize();
                interp = Vector2.Multiply(interp, (float)Math.PI);
                bullet.Rotation = Math.Atan2(-interp.Y, -interp.X);
                shootingRotation = bullet.Rotation;
                GameMain.CurrentWorld.AddEntity(bullet);
            }
#if DEBUG
            if(MyKeyboard.KeyDebug1.IsToggled)
            {
                EntityTurret turret = new EntityTurret(PosX + 0.25f, PosY + 0.25f);
                GameMain.CurrentWorld.AddEntity(turret);
            }
#endif
            if(MyKeyboard.KeyBuyTurret.IsPressed)
            {
                if(this.Money >= 2000)
                {
                    EntityTurret turret = new EntityTurret(PosX + 0.25f, PosY + 0.25f);
                    GameMain.CurrentWorld.AddEntity(turret);
                    this.Money -= 2000;
                }
            }
        }

        public override void Draw()
        {
            if (!PreDraw())
            {
                return;
            }
            if (!MyKeyboard.KeyShoot.IsPressed)
            {
                if (MyKeyboard.KeyMoveDown.IsPressed && !MyKeyboard.KeyMoveUp.IsPressed)
                {
                    if (MyKeyboard.KeyMoveRight.IsPressed && !MyKeyboard.KeyMoveLeft.IsPressed)
                    {
                        MovementTextureState = EnumSheetPlayer.DownRight;
                    }
                    else if (MyKeyboard.KeyMoveLeft.IsPressed && !MyKeyboard.KeyMoveRight.IsPressed)
                    {
                        MovementTextureState = EnumSheetPlayer.DownLeft;
                    }
                    else
                    {
                        MovementTextureState = EnumSheetPlayer.Down;
                    }
                }
                else if (MyKeyboard.KeyMoveUp.IsPressed && !MyKeyboard.KeyMoveDown.IsPressed)
                {
                    if (MyKeyboard.KeyMoveRight.IsPressed && !MyKeyboard.KeyMoveLeft.IsPressed)
                    {
                        MovementTextureState = EnumSheetPlayer.UpRight;
                    }
                    else if (MyKeyboard.KeyMoveLeft.IsPressed && !MyKeyboard.KeyMoveRight.IsPressed)
                    {
                        MovementTextureState = EnumSheetPlayer.UpLeft;
                    }
                    else
                    {
                        MovementTextureState = EnumSheetPlayer.Up;
                    }
                }
                else if (MyKeyboard.KeyMoveLeft.IsPressed)
                {
                    MovementTextureState = EnumSheetPlayer.Left;
                }
                else if (MyKeyboard.KeyMoveRight.IsPressed)
                {
                    MovementTextureState = EnumSheetPlayer.Right;
                }
                else
                {
                    animationFrame = 0;
                }
            }
            else
            {
                double degreesRotation = 180.0d / Math.PI * -shootingRotation;
                if (shootingRotation > 0)
                {
                    degreesRotation = 360 + degreesRotation;
                }
                if (degreesRotation <= 22.5)
                {
                    MovementTextureState = EnumSheetPlayer.Right;
                }
                else if (degreesRotation <= 67.5)
                {
                    MovementTextureState = EnumSheetPlayer.UpRight;
                }
                else if (degreesRotation <= 112.5)
                {
                    MovementTextureState = EnumSheetPlayer.Up;
                }
                else if (degreesRotation <= 157.5)
                {
                    MovementTextureState = EnumSheetPlayer.UpLeft;
                }
                else if (degreesRotation <= 202.5)
                {
                    MovementTextureState = EnumSheetPlayer.Left;
                }
                else if (degreesRotation <= 247.5)
                {
                    MovementTextureState = EnumSheetPlayer.DownLeft;
                }
                else if (degreesRotation <= 292.5)
                {
                    MovementTextureState = EnumSheetPlayer.Down;
                }
                else if (degreesRotation <= 340.5)
                {
                    MovementTextureState = EnumSheetPlayer.DownRight;
                }
                else
                {
                    MovementTextureState = EnumSheetPlayer.Right;
                }
            }
            ActualDraw();
        }

        public override Rectangle GetCurrentSourceRectangle()
        {
            //TODO KOMENTY!!!
            return currentTexture.GetCurrentSourceRectangle(animationFrame, (int)MovementTextureState);
        }
        
    }
}
