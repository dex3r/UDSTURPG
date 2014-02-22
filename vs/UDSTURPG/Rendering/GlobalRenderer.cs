using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
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
            if(GameMain.CurrentDrawingState != EnumDrawingState.World)
            {
                throw new Exception("Wrong rendering state");
            }
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    ChunkRenderer.RenderChunk(world.GetChunk(x, y));
                }
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach(Entity entity in world.Entities)
            {
                entity.Draw();
            }
            sw.Stop();
            GameMain.time = sw.ElapsedTicks;
        }

        public static void DrawEntity(Texture2D texture, float posX, float posY, Rectangle sourceRectangle, float depth, Color color)
        {
            GameMain.SpriteBatch.Draw(texture, new Vector2(posX * 64.0f, posY * 64.0f), sourceRectangle, color, 0, Vector2.Zero, 2.0f, SpriteEffects.None, depth);
        }

        public static void DrawBlock(Texture2D texture, float posX, float posY, Rectangle sourceRectangle, float depth)
        {
            GameMain.SpriteBatch.Draw(texture, new Vector2(posX * 64.0f, posY * 64.0f), sourceRectangle, Color.White, 0, Vector2.Zero, 2.0f, SpriteEffects.None, depth);
        }

        /// <summary>
        /// Rysowanie elementu GUI
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="posX">Pozycja X w zakresie od 0.0 do 1.0</param>
        /// <param name="posY">Pozycja Y w zakresie od 0.0 do 1.0</param>
        /// <param name="sourceRectangle"></param>
        /// <param name="depth"></param>
        /// <param name="color"></param>
        public static void DrawGuiElement(Texture2D texture, float posX, float posY, Rectangle sourceRectangle, float depth, Color color)
        {
            if(GameMain.CurrentDrawingState != EnumDrawingState.GUI)
            {
                throw new Exception("Wrong rendering state");
            }
            GameMain.SpriteBatch.Draw(texture, new Vector2(GameMain.SpriteBatch.GraphicsDevice.Viewport.Width / posX, GameMain.SpriteBatch.GraphicsDevice.Viewport.Height / posY), sourceRectangle, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, depth);
        }

        public static void DrawConsole()
        {
            throw new NotImplementedException();
        }
    }
}
