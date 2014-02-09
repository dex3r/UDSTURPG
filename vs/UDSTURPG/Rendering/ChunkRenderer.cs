using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.Main;
using RPG.Worlds;
using RPG.Blocks;
using RPG.Textures;

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
            for (ushort cx = 0; cx < Chunk.CHUNK_SIZE; cx++)
            {
                for (ushort cy = 0; cy < Chunk.CHUNK_SIZE; cy++)
                {
                    GameMain.SpriteBatch.Draw(Block.Blocks[chunk[cx, cy]].GetTexture(chunk, cx, cy), new Vector2(cx * 31, cy * 31), Textures2D.RFloor, Color.White);

                    GameMain.SpriteBatch.Draw(Block.Blocks[chunk[0, cy]].GetTexture(chunk, 0, cy), new Vector2(0, cy * 31), Textures2D.RWall, Color.White);
                    
                }
                GameMain.SpriteBatch.Draw(Block.Blocks[chunk[cx, 0]].GetTexture(chunk, cx, 0), new Vector2(cx * 31, 0), Textures2D.RWall, Color.White);
                GameMain.SpriteBatch.Draw(Block.Blocks[chunk[cx, (Chunk.CHUNK_SIZE - 1)]].GetTexture(chunk, cx, (Chunk.CHUNK_SIZE - 1)), new Vector2(cx * 31, (Chunk.CHUNK_SIZE - 1) * 31), Textures2D.RWall, Color.White);
            }
        }
    }
}
