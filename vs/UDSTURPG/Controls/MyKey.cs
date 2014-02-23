using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace RPG.Controls
{
    public class MyKey
    {
        private string name;
        private bool isPressed = false;
        private bool wasPressed;
        private bool isToggled;
        private int toggleRepeatRate;
        private int currentToggleTicks;
        private List<Keys> registeredKeys;
        private List<EnumMouseButtons> registeredMouseButtons;

        //!? Properties region
        #region PROPERTIES
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool IsPressed
        {
            get { return isPressed; }
        }
        public bool WasPressed
        {
            get { return wasPressed; }
        }
        public bool IsToggled
        {
            get { return isToggled; }
        }
        /// <summary>
        /// Z jaką częstotliwością (ticks) ma zmieniaś się stan IsToggled dla danego przycisku
        /// -1 (domyślne) dla przełączenia jednorazowego
        /// </summary>
        public int ToggleRepeatRate
        {
            get { return toggleRepeatRate; }
        }
        public List<Keys> RegisteredKeys
        {
            get { return registeredKeys; }
            set { registeredKeys = value; }
        }
        public List<EnumMouseButtons> RegisteredMouseButtons
        {
            get { return registeredMouseButtons; }
            set { registeredMouseButtons = value; }
        }
        #endregion
        //!? END of properties region

        public MyKey(string keyName)
        {
            MyKeyboard.AllKeys.Add(this);
            this.Name = keyName;
            registeredKeys = new List<Keys>(1);
            registeredMouseButtons = new List<EnumMouseButtons>(0);
            toggleRepeatRate = -1;
        }

        public MyKey(string keyName, Keys key)
            : this(keyName)
        {
            registeredKeys.Add(key);
        }

        public MyKey(string keyName, EnumMouseButtons mouseButton)
            : this(keyName)
        {
            registeredMouseButtons.Add(mouseButton);
        }

        public MyKey(string keyName, List<Keys> keys)
            : this(keyName)
        {
            registeredKeys = keys;
        }

        public MyKey SetRepeatRate(int repeatRate)
        {
            this.toggleRepeatRate = repeatRate;
            return this;
        }

        public void Update(KeyboardState kstate)
        {
            wasPressed = IsPressed;
            isPressed = false;
            for (int i = 0; i < registeredKeys.Count; i++)
            {
                if (kstate.IsKeyDown(registeredKeys[i]))
                {
                    isPressed = true;
                    break;
                }
            }
            if (!isPressed)
            {
                foreach (EnumMouseButtons mouseButton in registeredMouseButtons)
                {
                    if (MyMouse.IsButtonPressed(mouseButton))
                    {
                        isPressed = true;
                        break;
                    }
                }
            }
            if (toggleRepeatRate == -1)
            {
                isToggled = IsPressed && !wasPressed;
            }
            else
            {
                isToggled = false;
                if (currentToggleTicks > 0)
                {
                    currentToggleTicks++;
                }
                if (currentToggleTicks >= toggleRepeatRate)
                {
                    currentToggleTicks = 0;
                }
                if (IsPressed && currentToggleTicks == 0)
                {
                    isToggled = true;
                    currentToggleTicks++;
                }
            }
        }

        public MyKey RegisterKey(Keys key)
        {
            if (!registeredKeys.Contains(key))
            {
                registeredKeys.Add(key);
            }
            return this;
        }

        public MyKey RegisterMouseButton(EnumMouseButtons mouseButton)
        {
            if (!registeredMouseButtons.Contains(mouseButton))
            {
                registeredMouseButtons.Add(mouseButton);
            }
            return this;
        }

        public bool UnregisterKey(Keys key)
        {
            return registeredKeys.Remove(key);
        }

        public bool UnregisterMouseButton(EnumMouseButtons mouseButton)
        {
            return registeredMouseButtons.Remove(mouseButton);
        }
    }
}