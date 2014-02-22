using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG.GUI
{
    public class GUIElement
    {
        private int posX;
        private int posY;
        private int width;
        private int height;
        private bool isVisible;
        private float drawingDepth;
        private bool clickableThrough;

        #region properties
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        public float DrawingDepth
        {
            get { return drawingDepth; }
            set { drawingDepth = value; }
        }
        /// <summary>
        /// Czy możesz kliknąć na element, który znajduje się pod spodem
        /// </summary>
        public bool ClickableThrough
        {
            get { return clickableThrough; }
            set { clickableThrough = value; }
        }
        #endregion

        public GUIElement(int posX, int posY, int width, int height)
        {
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
            throw new NotImplementedException();
        }

        public virtual void Update()
        {
            if (!isVisible)
            {
                return;
            }
        }

        public virtual void Draw()
        {
            if (!isVisible)
            {
                return;
            }
        }
    }
}
