using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RPG.Controls;
using RPG.Textures2D;
using RPG.Main;
using Microsoft.Xna.Framework;

namespace RPG.Entities
{
    public class Player : Living
    {
        private float maxSpeed;

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }
        private Vector2 currentSpeed;

        public Vector2 CurrentSpeed
        {
            get { return currentSpeed; }
            set { currentSpeed = value; }
        }
        private float accelerate;

        public float Accelerate
        {
            get { return accelerate; }
            set { accelerate = value; }
        }

        private EnumSheetPlayer movementState;
        public EnumSheetPlayer MovementState
        {
            get { return movementState; }
            set { movementState = value; }
        }

        public Player(float posX, float posY)
            : base(posX, posY)
        {
            currentSpeed = new Vector2(0, 0);
            CurrentTexture = MyTexture.PlayerLordUpShooting;
            maxSpeed = 0.045f;
            accelerate = 0.03f;
        }

        public override void Update()
        {
            base.Update();
            if (MyKeyboard.KeyMoveDown.IsPressed)
            {
                if (currentSpeed.Y < maxSpeed)
                {
                    currentSpeed.Y += accelerate;
                }
                PosY += currentSpeed.Y;
            }
            if (MyKeyboard.KeyMoveUp.IsPressed)
            {
                if (currentSpeed.Y < maxSpeed)
                {
                    currentSpeed.Y += accelerate;
                }
                PosY -= currentSpeed.Y;
            }
            if (MyKeyboard.KeyMoveLeft.IsPressed)
            {
                if (currentSpeed.X < maxSpeed)
                {
                    currentSpeed.X += accelerate;
                }
                PosX -= currentSpeed.X;
            }
            if (MyKeyboard.KeyMoveRight.IsPressed)
            {
                if (currentSpeed.X < maxSpeed)
                {
                    currentSpeed.X += accelerate;
                }
                PosX += currentSpeed.X;
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (MyKeyboard.KeyMoveDown.IsPressed && !MyKeyboard.KeyMoveUp.IsPressed)
            {
                if (MyKeyboard.KeyMoveRight.IsPressed && !MyKeyboard.KeyMoveLeft.IsPressed)
                {
                    MovementState = EnumSheetPlayer.DownRight;
                }
                else if (MyKeyboard.KeyMoveLeft.IsPressed && !MyKeyboard.KeyMoveRight.IsPressed)
                {
                    MovementState = EnumSheetPlayer.DownLeft;
                }
                else
                {
                    MovementState = EnumSheetPlayer.Down;
                }
            }
            else if(MyKeyboard.KeyMoveUp.IsPressed && !MyKeyboard.KeyMoveDown.IsPressed)
            {
                if (MyKeyboard.KeyMoveRight.IsPressed && !MyKeyboard.KeyMoveLeft.IsPressed)
                {
                    MovementState = EnumSheetPlayer.UpRight;
                }
                else if (MyKeyboard.KeyMoveLeft.IsPressed && !MyKeyboard.KeyMoveRight.IsPressed)
                {
                    MovementState = EnumSheetPlayer.UpLeft;
                }
                else
                {
                    MovementState = EnumSheetPlayer.Up;
                }
            }
            else if(MyKeyboard.KeyMoveLeft.IsPressed)
            {
                MovementState = EnumSheetPlayer.Left;
            }
            else if(MyKeyboard.KeyMoveRight.IsPressed)
            {
                MovementState = EnumSheetPlayer.Right;
            }
            GameMain.SpriteBatch.Draw(currentTexture.Texture, new Vector2((int)(posX * 64), (int)(posY * 64)), currentTexture.GetCurrentSourceRectangle(animationFrame, (int)MovementState), Color.White, 0, new Vector2(), 2.0f, SpriteEffects.None, currentTexture.DepthOfDrawing);
        }
    }
}
