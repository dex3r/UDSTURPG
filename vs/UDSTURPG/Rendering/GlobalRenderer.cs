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
using RPG.Entities;
using RPG.Textures2D;

namespace RPG.Rendering
{
    public static class GlobalRenderer
    {
        public static void DrawWorld(World world)
        {
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    ChunkRenderer.RenderChunk(world.GetChunk(x, y));
                }
            }
            foreach(Entity entity in world.Entities)
            {
                entity.Draw();
            }
        }

        public static void DrawEntity(Texture2D texture, float posX, float posY, Rectangle sourceRectangle, float depth)
        {
            GameMain.SpriteBatch.Draw(texture, new Vector2(posX * 64.0f, posY * 64.0f), sourceRectangle, Color.White, 0, Vector2.Zero, 2.0f, SpriteEffects.None, depth);
        }

        public static void DrawBlock(Texture2D texture, float posX, float posY, Rectangle sourceRectangle, float depth)
        {
            GameMain.SpriteBatch.Draw(texture, new Vector2(posX * 64.0f, posY * 64.0f), sourceRectangle, Color.White, 0, Vector2.Zero, 2.0f, SpriteEffects.None, depth);
        }

        public static void DrawConsole()
        {
            throw new NotImplementedException();
        }
    }
}
