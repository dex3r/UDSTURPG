﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RPG.Main;

namespace RPG.Worlds
{
    public class Chunk
    {
        /// <summary>
        /// Rozmiar chunku, "const" dla wydajności
        /// </summary>
        public const int CHUNK_SIZE_X = 22;
        public const int CHUNK_SIZE_Y = 12;

        /// <summary>
        ///  ID obiektów, tablice jednowymiarowe dla wydajności
        /// </summary>
        private byte[][] chunkGround;
        private UInt16[][] chunkGroundMeta;
        private int x;
        private int y;
        private World worldObj;
        /// <summary>
        /// Nie robić żadnych innych referencji do tego pola
        /// </summary>
        private RenderTarget2D renderTarget;
        private bool needsRedrawing;

        //!? Properties region
        #region PROPERTIES
        /// <summary>
        ///  Metadane obiektów
        /// </summary>
        public UInt16[][] ChunkGroundMeta
        {
            get { return chunkGroundMeta; }
        }
        /// <summary>
        /// Zawsze zwraca true gdy RenderTarget jest pusty
        /// </summary>
        public bool NeedsRedrawing
        {
            get { return needsRedrawing; }
            set { needsRedrawing = value; }
        }
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public World WorldObj
        {
            get { return worldObj; }
            set { worldObj = value; }
        }
        public RenderTarget2D RenderTarget
        {
            get { return renderTarget; }
            set { renderTarget = value; }
        }
        #endregion
        //!? END of properties region

        public byte this[ushort x, ushort y]
        {
            get { return chunkGround[x][y]; }
            set
            {
                chunkGround[x][y] = value;
            }
        }

        public UInt16 GetMeta(ushort x, ushort y)
        {
            return chunkGroundMeta[x][y];
        }

        public void SetMeta(UInt16 value, ushort x, ushort y)
        {
            chunkGroundMeta[x][y] = value;
        }

        public Chunk(World world, int x, int y)
        {
            this.WorldObj = world;
            this.x = x;
            this.y = y;

            chunkGround = new byte[CHUNK_SIZE_X][];
            chunkGroundMeta = new UInt16[CHUNK_SIZE_X][];
            for (int i = 0; i < CHUNK_SIZE_X; i++)
            {
                chunkGround[i] = new byte[CHUNK_SIZE_Y];
                chunkGroundMeta[i] = new UInt16[CHUNK_SIZE_Y];
            }
            ResetChunkData(2);
        }

        /// <summary>
        /// Reset wszystkich danych w chunku
        /// </summary>
        /// <param name="id">Id pola</param>
        public void ResetChunkData(byte id)
        {
            for (int i = 0; i < CHUNK_SIZE_X; i++)
            {
                for (int j = 0; j < CHUNK_SIZE_Y; j++)
                {
                    chunkGround[i][j] = id;
                    chunkGroundMeta[i][j] = 0;
                }
            }
        }
    }
}
