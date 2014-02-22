using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG.GUI
{
    public class GUIElementText : GUIElement
    {
        private string text;
        
        #region properties
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        #endregion

        public GUIElementText(int posX, int posY, int width, int height) : base(posX, posY, width, height)
        {
            
        }

        public override void Draw()
        {
            
        }

        public GUIElementText SetText(string text)
        {
            this.text = text;
            return this;
        }
    }
}
