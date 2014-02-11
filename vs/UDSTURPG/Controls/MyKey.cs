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
        public string Name { get; private set; }
        private bool isPressed = false;
        public bool IsPressed
        {
            get
            {
                return isPressed;
            }
        }
        private bool wasPressed;
        public bool WasPressed
        {
            get { return wasPressed; }
        }

        private bool isToggled;
        public bool IsToggled
        {
            get { return isToggled; }
        }

        private int toggleRepeatRate;
        /// <summary>
        /// Z jaką częstotliwością (ticks) ma zmieniaś się stan IsToggled dla danego przycisku
        /// -1 (domyślne) dla przełączenia jednorazowego
        /// </summary>
        public int ToggleRepeatRate
        {
            get { return toggleRepeatRate; }
        }

        private int currentToggleTicks;

        public List<Keys> registeredKeys { get; private set; }
        public List<EnumMouseButtons> registeredMouseButtons { get; private set; }

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
