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
        public static Texture2D Wall;

        public static void Load(ContentManager cm)
        {
            Wall = cm.Load<Texture2D>(@"gfx\tiled\Tileset-Wall");
        }

        public static readonly Vector2 TailsetWall = new Vector2(31,55);



    }
}
