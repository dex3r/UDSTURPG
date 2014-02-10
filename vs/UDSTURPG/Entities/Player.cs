using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RPG.Controls;
using Microsoft.Xna.Framework;

namespace RPG.Entities
{
    public class Player : Living
    {
        private float maxSpeed = 0.5f;

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
        private float accelerate = 0.03f;

        public float Accelerate
        {
            get { return accelerate; }
            set { accelerate = value; }
        }

        public Player() : base()
        {
            currentSpeed = new Vector2(0,0);
            MyKeyboard.KeyMoveUp.ButtonDownEvent += KeyMoveUp_ButtonUpEvent;
            MyKeyboard.KeyMoveDown.ButtonDownEvent += KeyMoveDown_ButtonDownEvent;
            MyKeyboard.KeyMoveLeft.ButtonDownEvent += KeyMoveLeft_ButtonDownEvent;
            MyKeyboard.KeyMoveRight.ButtonDownEvent += KeyMoveRight_ButtonDownEvent;
        }

        void KeyMoveRight_ButtonDownEvent(MyKey key)
        {
            if(currentSpeed.X < maxSpeed)
            {
                currentSpeed.X += accelerate;
            }
            PosX += currentSpeed.X;
        }

        void KeyMoveLeft_ButtonDownEvent(MyKey key)
        {
            if (currentSpeed.X < maxSpeed)
            {
                currentSpeed.X += accelerate;
            }
            PosX -= currentSpeed.X;
        }

        void KeyMoveDown_ButtonDownEvent(MyKey key)
        {
            if (currentSpeed.Y < maxSpeed)
            {
                currentSpeed.Y += accelerate;
            }
            PosY -= currentSpeed.Y;
        }

        void KeyMoveUp_ButtonUpEvent(MyKey key)
        {
            if (currentSpeed.Y < maxSpeed)
            {
                currentSpeed.Y += accelerate;
            }
            PosY += currentSpeed.Y;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
