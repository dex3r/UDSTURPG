using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;

namespace RPG.Rendering
{
    public class FontSmall : Font
    {
        public FontSmall(MyTexture texture)
            : base(texture)
        {
            Chars = new string[3];
            Chars[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ    ";
            Chars[1] = "abcdefghijklmnopqrstuvwxyz    ";
            Chars[2] = "0123456789-.!?/%$\\=*+,;:()&#\"'";
        }
    }
}
