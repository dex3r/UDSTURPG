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
        

        public static void Update()
        {
            KeyboardState kstate = Keyboard.GetState();
            for(int i = 0; i < allKeys.Count; i++)
            {
                allKeys[i].Update(kstate);
            }
        }
    }
}
