using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;

namespace RPG.Rendering
{
    public class FontBig : Font
    {
        public FontBig(MyTexture texture)
            : base(texture)
        {
            Chars = new string[2];
            Chars[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ    ";
            Chars[1] = "0123456789-.!?/%$\\=*+,;:()&#\"'";
        }

        public override void DrawString(string stringToDraw, int posX, int posY)
        {
            stringToDraw = stringToDraw.ToUpper();
            Do_DrawString(stringToDraw, posX, posY);
        }
    }
}
