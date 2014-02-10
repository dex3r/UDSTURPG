using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RPG.Worlds;
using RPG.Main;
using RPG.Blocks;

namespace RPG.Rendering
{
    public static class GlobalRenderer
    {
        private static Chunk c;
        private static Vector2 v;

        public static void Draw(World world)
        {
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    ChunkRenderer.RenderChunk(world.GetChunk(x, y));
                }
            }
        }

        internal static void DrawConsole()
        {
            throw new NotImplementedException();
        }
    }
}
