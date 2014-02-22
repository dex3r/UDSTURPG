using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;
using RPG.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Rendering
{
    public abstract class Font
    {
        #region static
        public static readonly FontBig BigGold = new FontBig(MyTexture.FontBigGold);
        public static readonly FontBig BigBlue = new FontBig(MyTexture.FontBigGold);
        public static readonly FontBig BigGray = new FontBig(MyTexture.FontBigGold);
        public static readonly FontBig BigRed = new FontBig(MyTexture.FontBigGold);
        public static readonly FontSmall SmallWhite = new FontSmall(MyTexture.FontSmallWhite);
        public static readonly FontSmall SmallBlack = new FontSmall(MyTexture.FontSmallBlack);
        public static readonly FontSmall SmallGold = new FontSmall(MyTexture.FontSmallGold);
        #endregion

        private string[] chars;
        private MyTexture texture;
        private int charWidth;
        private int charHeight;

        #region properties
        public string[] Chars
        {
            get { return chars; }
            protected set { chars = value; }
        }
        public MyTexture Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public int CharWidth
        {
            get { return charWidth; }
            set { charWidth = value; }
        }
        public int CharHeight
        {
            get { return charHeight; }
            set { charHeight = value; }
        }
        #endregion

        public Font(MyTexture texture)
        {
            this.texture = texture;
            charWidth = 8;
            charHeight = 8;
        }

        public virtual void DrawString(string stringToDraw, int posX, int posY)
        {
            Do_DrawString(stringToDraw, posX, posY);
        }

        protected virtual void Do_DrawString(string stringToDraw, int posX, int posY)
        {
            char c;
            int pos = -1;
            int charLine = 0; 
            for(int i = 0; i < stringToDraw.Length; i++)
            {
                c = stringToDraw[i];
                if(c == ' ')
                {
                    continue;
                }
                for(int j = 0; j < chars.Length; j++)
                {
                    pos = chars[j].IndexOf(c);
                    if(pos != -1)
                    {
                        charLine = j;
                        break;
                    }
                }
                if(pos == -1)
                {
                    continue;
                }
                GameMain.SpriteBatch.Draw(texture.Texture, new Vector2(posX + (charWidth * i), posY), new Rectangle(pos * charWidth, charLine * charHeight, charWidth, charHeight), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.8f);
            }
        }
    }
}
