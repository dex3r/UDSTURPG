﻿using System;
using System.Text;
using RPG.Controls;
using RPG.Rendering;
using RPG.Worlds;
using RPG.Textures;
using RPG.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG.Main
{
    public class GameMain : Game
    {
        #region static
        //!? Private:
        private static GraphicsDeviceManager graphicsDeviceManager;

        // Zmienne do prostego pomiaru FPS
        private static int currentFps;
        private static int lastFps;
        private static long lastSec;
        private static int updateTime;
        //! Wyodrębnienie poza Draw dla wydajności
        private static StringBuilder sb = new StringBuilder();

        //!? Public:
        private static World currentWorld;
        /// <summary>
        /// Świat który jest aktualnie wyświetlany (null jeżeli w menu etc)
        /// </summary>
        public static World CurrentWorld
        {
            get { return currentWorld; }
            set { currentWorld = value; }
        }

        public static SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// Wróć do pozycji kamery
        /// </summary>
        public static void BeginNormalDrawing()
        {
            BeginDrawingAndApplyTransformation(Matrix.Identity);
        }

        /// <summary>
        /// Zastosuj transofrmację (trans * Camera)
        /// </summary>
        /// <param name="transformation"></param>
        public static void BeginDrawingAndApplyTransformation(Matrix transformation)
        {
            SamplerState.PointWrap.MaxAnisotropy = 0;
            SamplerState.PointWrap.MaxMipLevel = 0;
            SamplerState.PointWrap.MipMapLevelOfDetailBias = 0;
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointWrap, null, null, null, transformation * Camera.Transform);
        }
        #endregion

        public GameMain()
            : base()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // Ustawianie fullscreena początkowego i rozdziałki jest teraz w obiekcjie options
            // Nie przenosić do Initialize!
            Options.Init(graphicsDeviceManager);
            // Vsync i fixedTimeStep:
            this.IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            currentWorld = new World();
            //Ustawienie pozycji okna
            Window.SetPosition(new Point(400, 100));
            //Początkowa pozycja kamery na środku rysowanego pola 
            Camera.X = 0;
            Camera.Y = 0;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Textures2D.Load(this.Content);
            Text.Load(this.Content);
            Text.LoadDefaultFont();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Options.KeyPressed(graphicsDeviceManager);

            Camera.Interaction(graphicsDeviceManager, GraphicsDevice);
            MyMouse.Update();
            MyKeyboard.Update();
            Camera.Update(GraphicsDevice);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Firebrick);
            if (lastSec != (long)gameTime.TotalGameTime.TotalSeconds)
            {
                lastFps = currentFps;
                currentFps = 0;
                lastSec = (long)gameTime.TotalGameTime.TotalSeconds;
            }
            currentFps++;

            //Wyświetlanie po transformacji
            BeginNormalDrawing();
            // Rysowanie świata i obiektów
            GlobalRenderer.Draw();

            SpriteBatch.End();

            //Wyświetlanie bez transformacji
            SpriteBatch.Begin();

            if (Console.isVisible)
            {
                GlobalRenderer.DrawConsole();
            }
            else
            {
#if DEBUG
                if (!Keyboard.GetState().IsKeyDown(Keys.F2))
#else
                if (Keyboard.GetState().IsKeyDown(Keys.F2))
#endif
                {

                    createDebugInfo();
                    SpriteBatch.DrawTextWithShaddow(Text.Log, new Vector2(0, 0));
                    Text.Log = "";
                }
            }
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void createDebugInfo()
        {
            sb.Clear();
            sb.Append("Mouse: ");
            sb.Append(Mouse.GetState().X);
            sb.Append(" ");
            sb.Append(Mouse.GetState().Y);
            sb.Append("\nFps: ");
            sb.Append(lastFps);
            sb.Append("\nRes:");
            sb.Append(GraphicsDevice.Viewport.Width);
            sb.Append("x");
            sb.Append(GraphicsDevice.Viewport.Height);
            sb.Append("\nCamera: ");
            sb.Append(Camera.X);
            sb.Append(" ");
            sb.Append(Camera.Y);
            sb.Append(" Zoom: ");
            sb.Append(Camera.Zoom);
            sb.Append("\n");
            sb.Append(Text.Log);
            Text.Log = sb.ToString();
        }
    }

}
