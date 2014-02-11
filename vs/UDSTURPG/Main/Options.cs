using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RPG.Controls;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Main
{
    public static class Options
    {

        private static byte resolutionChange = 0;
        /// <summary>
        /// Zmienna pomocnicza do poprawnej zmiany rozdzielczości na pełnym ekranie
        /// </summary>
        public static byte ResolutionChange
        {
            get { return Options.resolutionChange; }
            set { Options.resolutionChange = value; }
        }

        /// <summary>
        /// Status która rozdzielczość jest wybrana ( 0 = największa )
        /// </summary>
        private static int resolutionStatus;

        public static int ResolutionStatus
        {
            get { return Options.resolutionStatus; }
            set { Options.resolutionStatus = value; }
        }
        /// <summary>
        /// Tablica dostępnych rozdzielczości
        /// </summary>
        public static readonly int[,] resolution = new int[3, 2]
        {
            {1366,768},
            {1600,900},
            {1920,1080}

        };

        private static float scale;
        public static float Scale
        {
            get { return Options.scale; }
        }

        /// <summary>
        /// Initializowanie opcji gry (narazie jest tylko rozdzielczość)
        /// </summary>
        public static void Init(GraphicsDeviceManager graphicsDeviceManager)
        {
#if DEBUG
            resolutionStatus = 0;
            updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
#else
            //Rozdzielczość domyślna 1920x1080
            resolutionStatus = 0;
            updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
            //! Ustawianie FullScreena na początku
            toogleFullScreeen(graphicsDeviceManager);
#endif
        }

        /// <summary>
        /// Funkcja wywołująca akcje na przyskach
        //TODO DO ZMAINY NA GUI!
        /// </summary>
        public static void KeyPressed(GraphicsDeviceManager graphicsDeviceManager)
        {
            if (MyKeyboard.KeyF4.IsToggled)
            {
                toogleFullScreeen(graphicsDeviceManager);
            }
            if (MyKeyboard.KeyF5.IsToggled)
            {
                if (resolutionStatus < 2)
                {
                    resolutionStatus++;
                    updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
                    ResolutionChange = 1;
                }
            }
            if (MyKeyboard.KeyF6.IsToggled)
            {
                if (resolutionStatus > 0)
                {

                    resolutionStatus--;
                    updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
                    ResolutionChange = 1;
                }
            }
            //Zmiana rozdzielczości w przypadku uruchomionego fullscreena
            updateResolutionOnFullScreenFix(graphicsDeviceManager);
        }
        
        /// <summary>
        /// Toogle full screen z aplikowaniem zmian
        /// </summary>
        private static void toogleFullScreeen(GraphicsDeviceManager graphicsDeviceManager)
        {
            graphicsDeviceManager.ToggleFullScreen();
            graphicsDeviceManager.ApplyChanges();
        }
        /// <summary>
        /// Ustawianie rozdzielczości
        /// UWAGA! Do zmiany rozdzielczości na fullscreenie nie działa
        /// </summary>
        /// <param name="frameWidth">Szerokość ekranu</param>
        /// <param name="frameHeight">Wysokość ekranu</param>
        private static void updateResolution(GraphicsDeviceManager graphicsDeviceManager, int width, int height)
        {
            graphicsDeviceManager.PreferredBackBufferHeight = height;
            graphicsDeviceManager.PreferredBackBufferWidth = width;
            scale = (float)width / 1366.0f;
            graphicsDeviceManager.ApplyChanges();
            graphicsDeviceManager.GraphicsDevice.Clear(Color.Wheat);
            graphicsDeviceManager.GraphicsDevice.Viewport = new Viewport(graphicsDeviceManager.GraphicsDevice.Viewport.X, graphicsDeviceManager.GraphicsDevice.Viewport.Y, width, height);
            OpenTK.Graphics.OpenGL.GL.Viewport(0, 0, width, height);
            graphicsDeviceManager.ApplyChanges();

            //graphicsDeviceManager.ToggleFullScreen();
            //graphicsDeviceManager.ApplyChanges();
            //graphicsDeviceManager.ToggleFullScreen();
            //graphicsDeviceManager.ApplyChanges();
        }
        /// <summary>
        /// Naprawa błędów wywołanych zmianą rozdzielczości podczas fullScreena
        /// </summary>
        private static void updateResolutionOnFullScreenFix(GraphicsDeviceManager graphicsDeviceManager)
        {
            if (ResolutionChange == 1 && graphicsDeviceManager.IsFullScreen == true)
            {
                toogleFullScreeen(graphicsDeviceManager);
                ResolutionChange = 2;
            }
            if (ResolutionChange == 2)
            {
                toogleFullScreeen(graphicsDeviceManager);
                ResolutionChange = 0;
            }
        }

    }
}
