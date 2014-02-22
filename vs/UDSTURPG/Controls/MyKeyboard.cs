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
        private static KeyboardState keysState;
        private static MyKey keyToggleConsole = new MyKey("Toggle console", Keys.OemTilde);
        private static MyKey keyMoveUp = new MyKey("Move player up", Keys.W);
        private static MyKey keyMoveDown = new MyKey("Move player down", Keys.S);
        private static MyKey keyMoveLeft = new MyKey("Move player left", Keys.A);
        private static MyKey keyMoveRight = new MyKey("Move player right", Keys.D);
        private static MyKey keyShoot = new MyKey("Shoot", EnumMouseButtons.Left).RegisterKey(Keys.Space).SetRepeatRate(10);
        private static MyKey keyBuyTurret = new MyKey("Buy turret", Keys.F).SetRepeatRate(10);
        private static MyKey keyDebug1 = new MyKey("Debug key 1", EnumMouseButtons.Right);
        private static MyKey keyFullscreen = new MyKey("Fullscreen", Keys.F4);
        private static MyKey keyResUp = new MyKey("Resolution up", Keys.F5);
        private static MyKey keyResDown = new MyKey("Resolution down", Keys.F6);
        private static MyKey keyCollisionBoxDrawToggle = new MyKey("Collision box", Keys.F10);

        //!? Properties region
        #region PROPERTIES
        public static List<MyKey> AllKeys
        {
            get { return MyKeyboard.allKeys; }
            set { MyKeyboard.allKeys = value; }
        }
        public static KeyboardState KeysState
        {
            get { return MyKeyboard.keysState; }
        }
        public static MyKey KeyToggleConsole
        {
            get { return MyKeyboard.keyToggleConsole; }
        }
        public static MyKey KeyMoveUp
        {
            get { return MyKeyboard.keyMoveUp; }
        }
        public static MyKey KeyMoveDown
        {
            get { return MyKeyboard.keyMoveDown; }
        }
        public static MyKey KeyMoveLeft
        {
            get { return MyKeyboard.keyMoveLeft; }
        }
        public static MyKey KeyMoveRight
        {
            get { return MyKeyboard.keyMoveRight; }
        }
        public static MyKey KeyShoot
        {
            get { return MyKeyboard.keyShoot; }
        }
        public static MyKey KeyBuyTurret
        {
            get { return MyKeyboard.keyBuyTurret; }
        }
        public static MyKey KeyDebug1
        {
            get { return MyKeyboard.keyDebug1; }
        }
        public static MyKey KeyFullscreen
        {
            get { return MyKeyboard.keyFullscreen; }
        }
        public static MyKey KeyResUp
        {
            get { return MyKeyboard.keyResUp; }
        }
        public static MyKey KeyResDown
        {
            get { return MyKeyboard.keyResDown; }
        }
        public static MyKey KeyCollisionBoxDrawToggle
        {
            get { return MyKeyboard.keyCollisionBoxDrawToggle; }
        }
        #endregion
        //!? END of properties region

        public static void Update()
        {
            keysState = Keyboard.GetState();
            for (int i = 0; i < allKeys.Count; i++)
            {
                allKeys[i].Update(keysState);
            }
        }
    }
}
