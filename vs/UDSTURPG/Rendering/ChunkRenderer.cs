using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.Main;
using RPG.Worlds;
using RPG.Blocks;

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
            if (chunk.RenderTarget == null)
            {
                chunk.RenderTarget = new RenderTarget2D(GameMain.SpriteBatch.GraphicsDevice, ChunkRenderer.CHUNK_SURFACE_WIDTH, ChunkRenderer.CHUNK_SURFACE_HEIGHT, false, SurfaceFormat.Bgra5551, DepthFormat.None);
            }
            GameMain.SpriteBatch.GraphicsDevice.SetRenderTarget(chunk.RenderTarget);
            GameMain.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            GameMain.SpriteBatch.GraphicsDevice.Clear(Color.Transparent);
            for (ushort cx = 0; cx < Chunk.CHUNK_SIZE; cx++)
            {
                for (ushort cy = 0; cy < Chunk.CHUNK_SIZE; cy++)
                {
                    GameMain.SpriteBatch.Draw(Block.Blocks[chunk[cx, cy]].GetTexture(chunk, cx, cy), new Vector2(cx,cy), Color.White);
                }
            }
            GameMain.SpriteBatch.End();
            GameMain.SpriteBatch.GraphicsDevice.SetRenderTarget(null);
            chunk.NeedsRedrawing = false;
        }
    }
}
