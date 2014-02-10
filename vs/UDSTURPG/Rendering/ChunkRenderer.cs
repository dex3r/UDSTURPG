using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.Main;
using RPG.Worlds;
using RPG.Blocks;
using RPG.Textures2D;

namespace RPG.Rendering
{
    public static class ChunkRenderer
    {
        public const int CHUNK_SURFACE_WIDTH = Chunk.CHUNK_SIZE * 64 + 64;
        public const int CHUNK_SURFACE_HEIGHT = Chunk.CHUNK_SIZE * 32 + 16;

        /// <summary>
        /// Renderuje chunk do pamięci
        /// Nie zapomnij o batch.End() przed tą funkcją (wydajność) oraz batch.Begin() za
        /// </summary>
        /// <param name="chunk"></param>
        public static void RenderChunk(Chunk chunk)
        {
            MyTexture texture;
            Rectangle sourceRect;
            for (ushort cx = 0; cx < Chunk.CHUNK_SIZE; cx++)
            {
                for (ushort cy = 0; cy < Chunk.CHUNK_SIZE; cy++)
                {
                    texture = Block.Blocks[chunk[cx, cy]].GetTexture(chunk, cx, cy);
                    sourceRect = texture.GetCurrentSourceRectangle(0);
                    GameMain.SpriteBatch.Draw(Block.Blocks[chunk[cx, cy]].GetTexture(chunk, cx, cy).Texture, new Rectangle(cx * 31, (cy * 31) - (sourceRect).Height - 31, sourceRect.Width, sourceRect.Height), texture.GetCurrentSourceRectangle(0), Color.White, 0, new Vector2(), SpriteEffects.None, texture.DepthOfDrawing);
                }
            }
        }
    }
}
