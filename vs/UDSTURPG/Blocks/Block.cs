﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RPG.Main;
using RPG.Worlds;

namespace RPG.Blocks
{
    public class Block
    {
        #region Static

        private static Block[] blocks = new Block[256];
        public static Block[] Blocks
        {
            get { return Block.blocks; }
        }

        public static Block Wall = new Block(1, "Wall", Textures.Textures2D.TWall);

        #endregion
        #region Non-static

        private byte id;
        public byte Id
        {
            get { return id; }
        }

        private String name;
        public String Name
        {
            get { return name; }
        }

        /// <summary>
        /// Podstawowa tekstura dla tego bloku
        /// </summary>
        private Texture2D texture;

        public Block(byte id, String name)
        {
            this.id = id;
            this.name = name;
            Block.blocks[id] = this;
        }

        public Block(byte id, String name, Texture2D texture) : this(id, name)
        {
            this.texture = texture;
        }


        public virtual Texture2D GetTexture(Chunk chunk,ushort chunkX, ushort chunkY)
        {
            return texture;
        }

        public virtual void SetMeta(UInt16 value, int x, int y)
        {
            //GameMain.CurrentWorld.SetMeta(value, x, y);
        }
        #endregion
    }
}
