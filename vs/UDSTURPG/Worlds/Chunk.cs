using System;
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
        public const int CHUNK_SIZE = 16;

        /// <summary>
        ///  ID obiektów, tablice jednowymiarowe dla wydajności
        /// </summary>
        private byte[] chunkGround;

        private UInt16[] chunkGroundMeta;
        /// <summary>
        ///  Metadane obiektów
        /// </summary>
        public UInt16[] ChunkGroundMeta
        {
            get { return chunkGroundMeta; }
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public World WorldObj { get; private set; }
        /// <summary>
        /// Nie robić żadnych innych referencji do tego pola
        /// </summary>
        public RenderTarget2D RenderTarget { get; set; }

        private bool needsRedrawing;
        /// <summary>
        /// Zawsze zwraca true gdy RenderTarget jest pusty
        /// </summary>
        public bool NeedsRedrawing
        {
            get
            {
                return needsRedrawing;
            }
            set
            {
                needsRedrawing = value;
            }
        }

        public byte this[ushort x, ushort y]
        {
            get { return chunkGround[CHUNK_SIZE * y + x]; }
            set
            {
                chunkGround[CHUNK_SIZE * y + x] = value;
                MarkToRedraw();
            }
        }

        public UInt16 GetMeta(ushort x, ushort y)
        {
            return chunkGroundMeta[CHUNK_SIZE * y + x];
        }

        public void SetMeta(UInt16 value, ushort x, ushort y)
        {
            chunkGroundMeta[CHUNK_SIZE * y + x] = value;
            MarkToRedraw();
        }

        public Chunk(World world, int x, int y)
        {
            this.WorldObj = world;
            this.X = x;
            this.Y = y;

            chunkGround = new byte[CHUNK_SIZE * CHUNK_SIZE];
            chunkGroundMeta = new UInt16[CHUNK_SIZE * CHUNK_SIZE];
            ResetChunkData(2);
        }

        /// <summary>
        /// Reset wszystkich danych w chunku
        /// </summary>
        /// <param name="id">Id pola</param>
        public void ResetChunkData(byte id)
        {
            for (int i = 0; i < CHUNK_SIZE * CHUNK_SIZE; i++)
            {
                chunkGround[i] = id;
                chunkGroundMeta[i] = 0;
            }

            for (ushort i = 0; i < CHUNK_SIZE; i++)
            {
                this[i, CHUNK_SIZE - 1] = 1;
                this[i, 0] = 1;
            }
        }

        public void MarkToRedraw()
        {
            NeedsRedrawing = true;
        }
    }
}
