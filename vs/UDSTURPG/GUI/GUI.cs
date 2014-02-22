using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG.GUI
{
    public class GUI
    {
        private float posX;
        private float posY;
        private int width;
        private int height;
        private bool isVisible;

        #region properties
        /// <summary>
        /// Pozycja X GUI na ekranie w zakresie 0.0 do 1.0
        /// </summary>
        public float PosX
        {
            get { return posX; }
            set { this.posX = value; }
        }
        /// <summary>
        /// Pozycja Y GUI na ekranie w zakresie 0.0 do 1.0
        /// </summary>
        public float PosY
        {
            get { return posY; }
            set { this.posY = value; }
        }
        /// <summary>
        /// Szerokość w pixelach
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        /// <summary>
        /// Wysokość w pixelach
        /// </summary>
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
        #endregion

        public GUI(int width, int height, float posX = 0.0f, float posY = 0.0f)
        {
            this.width = width;
            this.height = height;
            this.posX = posX;
            this.posY = posY;
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
