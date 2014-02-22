﻿using System;
using System.Text;
using RPG.Controls;
using RPG.Rendering;
using RPG.Worlds;
using RPG.Textures2D;
using RPG.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.Entities;

namespace RPG.Main
{
    public class GameMain : Game
    {
        #region static
        //!? Private:
        public static GraphicsDeviceManager graphicsDeviceManager;

        // Zmienne do prostego pomiaru FPS
        private static int currentFps;
        private static int lastFps;
        private static long lastSec;
        private static int updateTime;
        //! Wyodrębnienie poza DrawWorld dla wydajności
        private static StringBuilder sb = new StringBuilder();
        private static StringBuilder sc = new StringBuilder();

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

        private static EntityPlayer currentPlayer;
        public static EntityPlayer CurrentPlayer
        {
            get { return GameMain.currentPlayer; }
            set { GameMain.currentPlayer = value; }
        }

        private static uint entitiesId = 0;

        public static uint EntitiesId
        {
            get { return GameMain.entitiesId; }
            set { GameMain.entitiesId = value; }
        }

        public static SpriteBatch SpriteBatch { get; private set; }

        private static EnumDrawingState currentDrawingState;
        public static EnumDrawingState CurrentDrawingState
        {
            get { return GameMain.currentDrawingState; }
        }

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
            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, transformation * Camera.Transform);
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
            this.Window.AllowUserResizing = true;
            GraphicsDevice.PresentationParameters.DepthStencilFormat = DepthFormat.None;

        }

        protected override void Initialize()
        {
            base.Initialize();
            currentDrawingState = EnumDrawingState.Unknown;
            currentWorld = new World();
            currentPlayer = new EntityPlayer(2, 2);
            currentWorld.Entities.Add(currentPlayer);
            //Ustawienie pozycji okna
            Window.SetPosition(new Point(400, 100));
            //Początkowa pozycja kamery na środku rysowanego pola 
            Camera.X = 0;
            Camera.Y = 0;
            SamplerState.PointWrap.MaxAnisotropy = 0;
            SamplerState.PointWrap.MaxMipLevel = 0;
            SamplerState.PointWrap.MipMapLevelOfDetailBias = 0;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Textures2D.MyTexture.LoadAll(this.Content);
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

            MyMouse.Update();
            MyKeyboard.Update();

            Camera.Interaction();
            Camera.Update(GraphicsDevice);
            
            if(currentWorld != null)
            {
                currentWorld.Update();
                MobsGenerator.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            currentDrawingState = EnumDrawingState.Unknown;
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
            currentDrawingState = EnumDrawingState.World;
            // Rysowanie świata i obiektów
            if (currentWorld != null)
            {
                GlobalRenderer.DrawWorld(currentWorld);
            }
            SpriteBatch.End();

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Matrix.CreateScale(2.0f, 2.0f, 1.0f));
            currentDrawingState = EnumDrawingState.GUI;

            Font.BigGold.DrawString("Siema ludzie: :) !", 150, 150);

            SpriteBatch.End();

            //Wyświetlanie bez transformacji
            SpriteBatch.Begin();
            currentDrawingState = EnumDrawingState.Normal;

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
                    createScoreInfo();
                    SpriteBatch.DrawTextWithShaddow(Text.Score, new Vector2(graphicsDeviceManager.PreferredBackBufferWidth - 180,0));
                    Text.Score = "";
                }
            }
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public static long time = 0;
        private void createDebugInfo()
        {
            sb.Clear();
            sb.Append("Mouse: ");
            sb.Append(Mouse.GetState().X);
            sb.Append(" ");
            sb.Append(Mouse.GetState().Y);
            sb.Append("\nMouse Relative");
            sb.Append(MyMouse.PositionRelativeX);
            sb.Append(" ");
            sb.Append(MyMouse.PositionRelativeY);
            sb.Append("\nFps: ");
            sb.Append(lastFps);
            sb.Append("\nRes: ");
            sb.Append(graphicsDeviceManager.PreferredBackBufferWidth);
            sb.Append("x");
            sb.Append(graphicsDeviceManager.PreferredBackBufferHeight);
            sb.Append("\nViewport Res: ");
            sb.Append(graphicsDeviceManager.GraphicsDevice.Viewport.Width);
            sb.Append("x");
            sb.Append(graphicsDeviceManager.GraphicsDevice.Viewport.Width);
            sb.Append("\nCamera: ");
            sb.Append(Camera.X);
            sb.Append(" ");
            sb.Append(Camera.Y);
            sb.Append("\nPlayer: ");
            sb.Append(currentPlayer.PosX);
            sb.Append(" ");
            sb.Append(currentPlayer.PosY);
            sb.Append(" ");
            sb.Append(currentPlayer.Rotation);
            sb.Append("\nEntities: ");
            sb.Append((currentWorld == null ? 0 : currentWorld.Entities.Count));
            sb.Append("\nEntities draw time: ");
            sb.Append(time);
            sb.Append(Text.Log);
            Text.Log = sb.ToString();
        }

        private void createScoreInfo()
        {
            sc.Clear();
            sc.Append("Score: ");
            sc.Append(currentPlayer.Score);
            sc.Append("\nHealth: ");
            sc.Append(currentPlayer.CurrentHp);
            sc.Append(" / ");
            sc.Append(currentPlayer.MaxHp);
            Text.Score = sc.ToString();
        }
    }

}
