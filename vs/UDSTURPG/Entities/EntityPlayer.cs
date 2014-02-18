using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RPG.Controls;
using RPG.Textures2D;
using RPG.Main;
using Microsoft.Xna.Framework;
using RPG.Rendering;

namespace RPG.Entities
{
    public class EntityPlayer : EntityLiving
    {
        private float acceleration;

        public float Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        private EnumSheetPlayer movementTextureState;
        public EnumSheetPlayer MovementTextureState
        {
            get { return movementTextureState; }
            set { movementTextureState = value; }
        }

        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public EntityPlayer(float posX, float posY)
            : base(posX, posY)
        {
            CurrentTexture = MyTexture.PlayerLordLard;
            maxSpeed = 0.05f;
            acceleration = 0.03f;
            SetCollisionBox(0, 0, 1.0f, 1.0f);
            MaxHp = 100;
            CurrentHp = MaxHp;
        }

        public override void Update()
        {
            BoundryCollision();
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
                currentVelocity += acceleration;
                //Camera.X = posX * 64 - GameMain.graphicsDeviceManager.PreferredBackBufferWidth / 2+32;
                //Camera.Y = posY * 64 - GameMain.graphicsDeviceManager.PreferredBackBufferHeight / 2+32;
            }
            else
            {
                if (currentVelocity > 0)
                {
                    currentVelocity -= acceleration;
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
            foreach (Entity en in GameMain.CurrentWorld.Entities)
                if (en is EntityMob)
                {
                    if (Collision(en))
                    {
                        en.MarketToDelete = true;
                        GameMain.CurrentPlayer.CurrentHp -= 10;
                    }
                }
            base.Update();
            if (MyKeyboard.KeyShoot.IsToggled)
            {
                //TODO Komenty!!
                EntityBullet bullet = new EntityBullet(GameMain.CurrentPlayer.PosX + 0.25f, GameMain.CurrentPlayer.PosY + 0.25f);
                bullet.CurrentVelocity = 0.1f;
                Vector2 interp = Vector2.Subtract(new Vector2((GameMain.CurrentPlayer.PosX + 0.45f) * 64, (GameMain.CurrentPlayer.PosY + 0.45f) * 64), new Vector2(MyMouse.PositionRelativeX, MyMouse.PositionRelativeY));
                interp.Normalize();
                interp = Vector2.Multiply(interp, (float)Math.PI);
                bullet.Rotation = Math.Atan2(-interp.Y, -interp.X);
                shootingRotation = bullet.Rotation;
                GameMain.CurrentWorld.AddEntity(bullet);
            }
            if(MyKeyboard.KeyDebug1.IsToggled)
            {
                EntityTurret turret = new EntityTurret(PosX + 0.25f, PosY + 0.25f);
                GameMain.CurrentWorld.AddEntity(turret);
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
