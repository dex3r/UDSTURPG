﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Rendering;
using RPG.Entities;
using RPG.Main;

namespace RPG.Controls
{
    public static class MyMouse
    {
        private static bool middleButtonStatus = false;
        public static int MouseHoldPositionX { get; private set; }
        public static int MouseHoldPositionY { get; private set; }
        public static int OverallScrollWheelValue { get; private set; }
        /// <summary>
        /// Różnica obrotów kółkiem od ostatniego Update 
        /// Uwaga! Jeden obrót to 120
        /// </summary>
        public static int ScrollWheelDelta { get; private set; }

        private static Vector2 positionRelative;

        private static MouseState currentMouseState;
        public static MouseState CurrentMouseState
        {
            get { return currentMouseState; }
        }

        private static bool wasLMBDown;
        public static bool WasLMBDown
        {
            get { return wasLMBDown; }
        }

        public static Vector2 PositionRelative
        {
            get { return MyMouse.positionRelative; }
            set { MyMouse.positionRelative = value; }
        }
        /// <summary>
        /// Update relatywnej pozycji myszy i kółka //!TEMP Tworzy śnieg po wciśnięciu LPM
        /// </summary>
        public static void Update()
        {
            currentMouseState = Mouse.GetState();
            ScrollWheelDelta = OverallScrollWheelValue - Mouse.GetState().ScrollWheelValue;
            OverallScrollWheelValue = Mouse.GetState().ScrollWheelValue;
            positionRelative.X = Camera.Transform.Translation.X * -1 + currentMouseState.X;
            positionRelative.Y = Camera.Transform.Translation.Y * -1 + currentMouseState.Y;

            if (currentMouseState.LeftButton == ButtonState.Pressed && !wasLMBDown)
            {
                EntityBullet bullet = new EntityBullet(GameMain.CurrentPlayer.PosX + 0.125f, GameMain.CurrentPlayer.PosY + 0.125f);
                bullet.CurrentVelocity = 0.05f;
                Vector2 interp = Vector2.Subtract(new Vector2((GameMain.CurrentPlayer.PosX + 0.125f) * 64, (GameMain.CurrentPlayer.PosY + 0.125f) * 64), new Vector2(currentMouseState.X, currentMouseState.Y));
                interp.Normalize();
                interp = Vector2.Multiply(interp, (float)Math.PI);
                bullet.Rotation = Math.Atan2(-interp.Y, -interp.X);
                GameMain.CurrentWorld.Entities.Add(bullet);
            }

            wasLMBDown = currentMouseState.LeftButton == ButtonState.Pressed;
        }


        public static bool ChceckMouseRectangle(int x1, int y1, int x2, int y2)
        {
            if (Mouse.GetState().X >= x1 && Mouse.GetState().X <= x2 && Mouse.GetState().Y >= y1 && Mouse.GetState().Y <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Fukncja pomocnicza służy do przesuwania obrazu po przytrzymaniu środkowego przycisku myszy
        /// </summary>
        /// <returns>True - ON, False - OFF</returns>
        public static bool ToogleMiddleButton()
        {
            if (middleButtonStatus == false && Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                MouseHoldPositionX = Mouse.GetState().X;
                MouseHoldPositionY = Mouse.GetState().Y;
                middleButtonStatus = true;
            }
            else if (middleButtonStatus == true && Mouse.GetState().MiddleButton == ButtonState.Released)
            {
                middleButtonStatus = false;
            }
            return middleButtonStatus;
        }
    }
}
