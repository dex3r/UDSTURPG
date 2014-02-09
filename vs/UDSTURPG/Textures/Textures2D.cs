using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace RPG.Textures
{
    public static class Textures2D
    {
        public static Texture2D TWall;
        public static Rectangle RWall = new Rectangle(0, 0, 31, 55);

        public static void Load(ContentManager cm)
        {
            TWall = cm.Load<Texture2D>(@"gfx\tiled\Tileset-Wall");
        }
    }
}
