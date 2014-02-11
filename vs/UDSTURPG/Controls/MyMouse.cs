using Microsoft.Xna.Framework;
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
        public static int MouseHoldPositionX { get; private set; }
        public static int MouseHoldPositionY { get; private set; }
        public static int OverallScrollWheelValue { get; private set; }
        /// <summary>
        /// Różnica obrotów kółkiem od ostatniego Update 
        /// Uwaga! Jeden obrót to 120
        /// </summary>
        public static int ScrollWheelDelta { get; private set; }

        private static MouseState currentMouseState;
        public static MouseState CurrentMouseState
        {
            get { return currentMouseState; }
        }

        private static float positionRelativeX;
        public static float PositionRelativeX
        {
            get { return positionRelativeX; }
        }

        private static float positionRelativeY;
        public static float PositionRelativeY
        {
            get { return positionRelativeY; }
        }


        /// <summary>
        /// Update relatywnej pozycji myszy i kółka //!TEMP Tworzy śnieg po wciśnięciu LPM
        /// </summary>
        public static void Update()
        {
            currentMouseState = Mouse.GetState();
            ScrollWheelDelta = OverallScrollWheelValue - Mouse.GetState().ScrollWheelValue;
            OverallScrollWheelValue = Mouse.GetState().ScrollWheelValue;
            positionRelativeX = Rendering.Camera.Transform.Translation.X * -1 * (float)Math.Pow(Options.Scale, -1) + Mouse.GetState().X * (float)Math.Pow(Options.Scale, -1);
            positionRelativeY = Rendering.Camera.Transform.Translation.Y * -1 * (float)Math.Pow(Options.Scale, -1) + Mouse.GetState().Y * (float)Math.Pow(Options.Scale, -1);
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

        public static bool IsButtonPressed(EnumMouseButtons mouseButton)
        {
            switch (mouseButton)
            {
                case EnumMouseButtons.Left:
                    return currentMouseState.LeftButton == ButtonState.Pressed;
                case EnumMouseButtons.Right:
                    return currentMouseState.RightButton == ButtonState.Pressed;
                case EnumMouseButtons.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Pressed;
                case EnumMouseButtons.One:
                    return currentMouseState.XButton1 == ButtonState.Pressed;
                case EnumMouseButtons.Two:
                    return currentMouseState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }
    }
}
