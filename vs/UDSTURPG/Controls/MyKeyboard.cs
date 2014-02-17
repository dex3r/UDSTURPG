using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace RPG.Controls
{
    public static class MyKeyboard
    {
        private static List<MyKey> allKeys = new List<MyKey>();
        public static List<MyKey> AllKeys
        {
            get { return MyKeyboard.allKeys; }
            set { MyKeyboard.allKeys = value; }
        }

        private static KeyboardState keysState;
        public static KeyboardState KeysState
        {
            get { return MyKeyboard.keysState; }
        }

        private static MyKey keyToggleConsole = new MyKey("Toggle console", Keys.OemTilde);
        public static MyKey KeyToggleConsole
        {
            get { return MyKeyboard.keyToggleConsole; }
        }

        private static MyKey keyMoveUp = new MyKey("Move player up", Keys.W);
        public static MyKey KeyMoveUp
        {
            get { return MyKeyboard.keyMoveUp; }
        }

        private static MyKey keyMoveDown = new MyKey("Move player down", Keys.S);
        public static MyKey KeyMoveDown
        {
            get { return MyKeyboard.keyMoveDown; }
        }

        private static MyKey keyMoveLeft = new MyKey("Move player left", Keys.A);
        public static MyKey KeyMoveLeft
        {
            get { return MyKeyboard.keyMoveLeft; }
        }

        private static MyKey keyMoveRight = new MyKey("Move player right", Keys.D);
        public static MyKey KeyMoveRight
        {
            get { return MyKeyboard.keyMoveRight; }
        }

        private static MyKey keyShoot = new MyKey("Shoot", EnumMouseButtons.Left).RegisterKey(Keys.Space).SetRepeatRate(10);
        public static MyKey KeyShoot
        {
            get { return MyKeyboard.keyShoot; }
        }

        private static MyKey keyDebug1 = new MyKey("Debug key 1", EnumMouseButtons.Right);
        public static MyKey KeyDebug1
        {
            get { return MyKeyboard.keyDebug1; }
        }

        private static MyKey keyF4 = new MyKey("Fullscreen", Keys.F4);
        public static MyKey KeyF4
        {
            get { return MyKeyboard.keyF4; }
        }

        private static MyKey keyF5 = new MyKey("Resolution up", Keys.F5);
        public static MyKey KeyF5
        {
            get { return MyKeyboard.keyF5; }
        }

        private static MyKey keyF6 = new MyKey("Resolution down", Keys.F6);
        public static MyKey KeyF6
        {
            get { return MyKeyboard.keyF6; }
        }


        private static MyKey keyF10 = new MyKey("Collision box", Keys.F10);
        public static MyKey KeyF10
        {
            get { return MyKeyboard.keyF10; }
        }
        private static bool keyF10Pressed;
        public static bool KeyF10Pressed
        {
            get { return MyKeyboard.keyF10Pressed; }
            set { MyKeyboard.keyF10Pressed = value; }
        }

        public static void Update()
        {
            keysState = Keyboard.GetState();
            for(int i = 0; i < allKeys.Count; i++)
            {
                allKeys[i].Update(keysState);
            }

            if (KeyF10.IsToggled)
                if (KeyF10Pressed)
                    KeyF10Pressed = false;
                else
                    KeyF10Pressed = true;
        }
    }
}
