﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.Controls;
using RPG.Main;

namespace RPG.Rendering
{
    public static class Camera
    {
        static private Matrix transform = Matrix.Identity;
        static private float x;
        static private float y;
        private static float scale;
        /// <summary>
        /// Szybkość przesuwania kamery
        /// </summary>
        private const int STEP = 7;
        /// <summary>
        /// Wielkość krawędzi do przesuwania ekranu
        /// </summary>
        private const int BORDERSIZE = 15;

        //!? Properties region
        #region PROPERTIES
        /// <summary>
        /// Matrix do wykonywania obliczeń
        /// </summary>
        public static Matrix Transform
        {
            get { return Camera.transform; }
            set { Camera.transform = value; }
        }
        /// <summary>
        /// Pozycja X kamrry
        /// </summary>
        public static float X
        {
            get { return Camera.x; }
            set { Camera.x = value; }
        }
        /// <summary>
        /// Pozycja Y kamery
        /// </summary>
        public static float Y
        {
            get { return Camera.y; }
            set { Camera.y = value; }
        }
        public static float Scale
        {
            get { return Camera.scale; }
            set { Camera.scale = value; }
        }
        #endregion
        //!? END of properties region

        /// <summary>
        /// Aktualizacja kamery
        /// </summary>
        static public void Update(GraphicsDevice graphicDevice)
        {
            //X = 300;
            //Y = 300;
            Transform = Matrix.CreateTranslation(X, Y, 0) *
                        Matrix.CreateScale(scale, scale, 1.0f);
        }

        /// <summary>
        /// Przesuwanie kamery na wszystkei sposoby
        /// </summary>
        static public void Interaction()
        {
            //if (MyMouse.ToogleMiddleButton() == false)
            //{

            //    if (graphicsDeviceManager.IsFullScreen == true)
            //    {
            //        if (MyMouse.ChceckMouseRectangle(0, 0, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height / graphicDevice.Viewport.Height * BORDERSIZE)) Camera.Y -= STEP;
            //        if (MyMouse.ChceckMouseRectangle(0, graphicDevice.Viewport.Height - graphicDevice.Viewport.Height / graphicDevice.Viewport.Height * BORDERSIZE, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height)) Camera.Y += STEP;
            //        if (MyMouse.ChceckMouseRectangle(0, 0, graphicDevice.Viewport.Width / graphicDevice.Viewport.Width * BORDERSIZE, graphicDevice.Viewport.Height)) Camera.X -= STEP;
            //        if (MyMouse.ChceckMouseRectangle(graphicDevice.Viewport.Width - graphicDevice.Viewport.Width / graphicDevice.Viewport.Width * BORDERSIZE, 0, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height)) Camera.X += STEP;
            //    }
            if (MyKeyboard.KeysState.IsKeyDown(Keys.Left))
            {
                Camera.X += STEP;
            }
            if (MyKeyboard.KeysState.IsKeyDown(Keys.Right))
            {
                Camera.X -= STEP;
            }
            if (MyKeyboard.KeysState.IsKeyDown(Keys.Up))
            {
                Camera.Y += STEP;
            }
            if (MyKeyboard.KeysState.IsKeyDown(Keys.Down))
            {
                Camera.Y -= STEP;
            }
            //}
            //else
            //{
            //    Camera.X -= (MyMouse.MouseHoldPositionX - Mouse.GetState().X) / 40;
            //    Camera.Y -= (MyMouse.MouseHoldPositionY - Mouse.GetState().Y) / 40;
            //}
        }
    }
}
