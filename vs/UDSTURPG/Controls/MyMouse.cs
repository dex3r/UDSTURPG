using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.Rendering;
using RPG.Entities;
using RPG.Main;

namespace RPG.Controls
{
    public static class MyMouse
    {
        private static int overallScrollWheelValue;
        private static int scrollWheelDelta;
        private static MouseState currentMouseState;
        private static float positionRelativeX;
        private static float positionRelativeY;

        //!? Properties region
        #region PROPERTIES
        public static int OverallScrollWheelValue
        {
            get { return MyMouse.overallScrollWheelValue; }
        }
        /// <summary>
        /// Różnica obrotów kółkiem od ostatniego Update 
        /// Uwaga! Jeden obrót to 120
        /// </summary>
        public static int ScrollWheelDelta
        {
            get { return MyMouse.scrollWheelDelta; }
            set { MyMouse.scrollWheelDelta = value; }
        }
        public static MouseState CurrentMouseState
        {
            get { return currentMouseState; }
        }
        public static float PositionRelativeX
        {
            get { return positionRelativeX; }
        }
        public static float PositionRelativeY
        {
            get { return positionRelativeY; }
        }
        #endregion
        //!? END of properties region

        /// <summary>
        /// Update relatywnej pozycji myszy i kółka //!TEMP Tworzy śnieg po wciśnięciu LPM
        /// </summary>
        public static void Update()
        {
            currentMouseState = Mouse.GetState();
            scrollWheelDelta = OverallScrollWheelValue - Mouse.GetState().ScrollWheelValue;
            overallScrollWheelValue = Mouse.GetState().ScrollWheelValue;
            positionRelativeX = (Rendering.Camera.Transform.Translation.X * -1 + Mouse.GetState().X) * (float)Math.Pow(Camera.Scale, -1);
            positionRelativeY = (Rendering.Camera.Transform.Translation.Y * -1 + Mouse.GetState().Y) * (float)Math.Pow(Camera.Scale, -1);
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
