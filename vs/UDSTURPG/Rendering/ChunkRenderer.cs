﻿using System;
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
                    GameMain.SpriteBatch.Draw(Block.Blocks[chunk[cx, cy]].GetTexture(chunk, cx, cy).Texture, new Vector2(cx * 64, (cy * 64) - (sourceRect).Height - 32), texture.GetCurrentSourceRectangle(0), Color.White, 0, new Vector2(), 2.0f, SpriteEffects.None, texture.DepthOfDrawing);
                }
            }
        }
    }
}
