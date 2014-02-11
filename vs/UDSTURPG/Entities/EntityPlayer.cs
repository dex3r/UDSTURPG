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
        private float maxSpeed;

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

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

        public EntityPlayer(float posX, float posY)
            : base(posX, posY)
        {
            CurrentTexture = MyTexture.PlayerLordLard;
            maxSpeed = 0.05f;
            acceleration = 0.03f;
        }

        public override void Update()
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
            rotation = Math.Atan2(y, x);
            if(x != 0 || y != 0)
            {
                currentVelocity += acceleration;
                Camera.X = posX * 64 - GameMain.graphicsDeviceManager.PreferredBackBufferWidth / 2+32;
                Camera.Y = posY * 64 - GameMain.graphicsDeviceManager.PreferredBackBufferHeight / 2+32;
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
            if(currentVelocity > maxSpeed)
            {
                currentVelocity = maxSpeed;
            }
            else if(currentVelocity < 0)
            {
                currentVelocity = 0;
            }
            base.Update();
        }

        public override void Draw()
        {
            if(!PreDraw())
            {
                return;
            }
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
            ActualDraw();
        }

        public override Rectangle GetCurrentSourceRectangle()
        {
            return currentTexture.GetCurrentSourceRectangle(animationFrame, (int)MovementTextureState);
        }
    }
}
