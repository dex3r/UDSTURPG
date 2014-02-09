using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RPG.Controls;

namespace RPG.Entities
{
    public class Player : Living 
    {
        public override Texture2D Texture
        {
            get
            {
                return null;
            }
        }

        private float movingSpeed;
        public float MovingSpeed
        {
          get { return movingSpeed; }
          set { movingSpeed = value; }
        }

        public Player() : base()
        {
            movingSpeed = 0.01f;
            MyKeyboard.KeyMoveUp.ButtonDownEvent += KeyMoveUp_ButtonUpEvent;
            MyKeyboard.KeyMoveDown.ButtonDownEvent += KeyMoveDown_ButtonDownEvent;
            MyKeyboard.KeyMoveLeft.ButtonDownEvent += KeyMoveLeft_ButtonDownEvent;
            MyKeyboard.KeyMoveRight.ButtonDownEvent += KeyMoveRight_ButtonDownEvent;
        }

        void KeyMoveRight_ButtonDownEvent(MyKey key)
        {
            PosX += movingSpeed;
        }

        void KeyMoveLeft_ButtonDownEvent(MyKey key)
        {
            PosX -= movingSpeed;
        }

        void KeyMoveDown_ButtonDownEvent(MyKey key)
        {
            PosY -= movingSpeed;
        }

        void KeyMoveUp_ButtonUpEvent(MyKey key)
        {
            PosY += movingSpeed;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
