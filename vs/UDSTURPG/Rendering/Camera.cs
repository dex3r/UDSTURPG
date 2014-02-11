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
        /// <summary>
        /// Matrix do wykonywania obliczeń
        /// </summary>
        public static Matrix Transform
        {
            get { return Camera.transform; }
            set { Camera.transform = value; }
        }
        static private float x;
        /// <summary>
        /// Pozycja X kamrry
        /// </summary>
        public static float X
        {
            get { return Camera.x; }
            set { Camera.x = value; }
        }
        static private float y;
        /// <summary>
        /// Pozycja Y kamery
        /// </summary>
        public static float Y
        {
            get { return Camera.y; }
            set { Camera.y = value; }
        }

        /// <summary>
        /// Szybkość przesuwania kamery
        /// </summary>
        private const int STEP = 7;
        /// <summary>
        /// Wielkość krawędzi do przesuwania ekranu
        /// </summary>
        private const int BORDERSIZE = 15;

        /// <summary>
        /// Aktualizacja kamery
        /// </summary>
        static public void Update(GraphicsDevice graphicDevice)
        {
            Transform = Matrix.CreateTranslation(-(graphicDevice.Viewport.Width / 2 + X), -(graphicDevice.Viewport.Height / 2 + Y), 0) *
                       Matrix.CreateTranslation(graphicDevice.Viewport.Width / 2, graphicDevice.Viewport.Height / 2, 0);
        }

        public static Matrix CreateVirtualTransofrmation(float virtualZoom)
        {
            return Matrix.CreateTranslation(-(GameMain.SpriteBatch.GraphicsDevice.Viewport.Width / 2 + X), -(GameMain.SpriteBatch.GraphicsDevice.Viewport.Height / 2 + Y), 0) *
                       Matrix.CreateTranslation(GameMain.SpriteBatch.GraphicsDevice.Viewport.Width / 2, GameMain.SpriteBatch.GraphicsDevice.Viewport.Height / 2, 0);
        }

        /// <summary>
        /// Przesuwanie kamery na wszystkei sposoby
        /// </summary>
        static public void Interaction(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicDevice)
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
            //    if (Keyboard.GetState().IsKeyDown(Keys.Left)) Camera.X -= STEP;
            //    if (Keyboard.GetState().IsKeyDown(Keys.Right)) Camera.X += STEP;
            //    if (Keyboard.GetState().IsKeyDown(Keys.Up)) Camera.Y -= STEP;
            //    if (Keyboard.GetState().IsKeyDown(Keys.Down)) Camera.Y += STEP;
            //}
            //else
            //{
            //    Camera.X -= (MyMouse.MouseHoldPositionX - Mouse.GetState().X) / 40;
            //    Camera.Y -= (MyMouse.MouseHoldPositionY - Mouse.GetState().Y) / 40;
            //}
        }
    }
}
