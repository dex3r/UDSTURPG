using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RPG.Main;
using RPG.Worlds;
using Microsoft.Xna.Framework;
using RPG.Textures2D;

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

        public static Block Wall = new Block(1, "Wall", MyTexture.Wall);
        public static Block Floor = new Block(2, "Floor", MyTexture.Floor);

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

        private MyTexture texture;

        public Block(byte id, String name)
        {
            this.id = id;
            this.name = name;
            Block.blocks[id] = this;
        }

        public Block(byte id, String name, MyTexture texture) : this(id, name)
        {
            this.texture = texture;
        }


        public virtual MyTexture GetTexture(Chunk chunk, ushort chunkX, ushort chunkY)
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
