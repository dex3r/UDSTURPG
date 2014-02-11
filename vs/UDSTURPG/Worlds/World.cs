using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Rendering;
using RPG.Entities;

namespace RPG.Worlds
{
    public class World
    {
        //TODO: pobieranie bloku cx metadanych
        private int size;
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// Ilość wszystkich chunków dla obecnej wielkości świata
        /// </summary>
        public int ChunkNumbers { get; private set; }
        /// <summary>
        /// Ilość chunków w jednym rzędzie (pierwiastek z ChunkNumbers)
        /// </summary>
        public int ChunksInRow { get; private set; }

        private Chunk[] chunks;

        private List<Entity> entities;
        public List<Entity> Entities
        {
            get { return entities; }
        }

        public World()
        {
            size = 16;
            ChunkNumbers = (int)Math.Pow(size / Chunk.CHUNK_SIZE, 2);
            ChunksInRow = size / Chunk.CHUNK_SIZE;
            chunks = new Chunk[ChunkNumbers];
            for (int x = 0; x < ChunksInRow; x++)
            {
                for (int y = 0; y < ChunksInRow; y++)
                {
                    chunks[ChunksInRow * y + x] = new Chunk(this, x, y);
                }
            }
            entities = new List<Entity>();
        }

        public Chunk GetChunk(int x, int y)
        {
            return chunks[ChunksInRow * y + x];
        }

        public void SetMeta(UInt16 value, int x, int y)
        {
            GetChunk(x / Chunk.CHUNK_SIZE, y / Chunk.CHUNK_SIZE).SetMeta(value, (ushort)(x % 16), (ushort)(y % 16));
        }

        public void Update()
        {
            List<Entity> entitiesToDelete = new List<Entity>();
            foreach (Entity entity in entities)
            {
                entity.Update();
                if(entity.MarketToDelete)
                {
                    entitiesToDelete.Add(entity);
                }
            }
            foreach(Entity entity in entitiesToDelete)
            {
                entities.Remove(entity);
            }
        }

        public void LoadWorld()
        { 
        
        }




        public static byte[,] world = {  //0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5
                                          {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//1
                                          {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},//2
                                          {1,2,2,1,2,2,2,2,1,2,1,2,2,1,2,1},//3
                                          {1,2,2,1,1,1,2,2,1,2,1,2,2,2,2,1},//4
                                          {1,2,2,1,2,2,2,1,1,2,1,2,2,1,2,1},//5
                                          {1,2,2,1,2,1,1,1,2,1,2,1,1,1,2,1},//6
                                          {1,2,2,1,1,1,2,2,2,1,2,1,2,2,2,1},//7
                                          {1,2,1,1,2,2,1,1,2,1,2,1,1,1,1,1},//8
                                          {1,1,1,1,2,1,2,2,2,1,2,1,2,2,2,1},//9
                                          {1,2,2,2,2,2,2,1,2,1,2,2,2,1,1,1},//10
                                          {1,2,2,1,1,1,2,2,2,1,1,1,2,2,2,1},//11
                                          {1,2,2,1,1,1,2,2,2,2,2,2,1,1,2,1},//12
                                          {1,1,1,1,2,2,1,1,1,2,2,1,2,1,2,1},//13
                                          {1,2,2,2,2,2,2,2,2,2,1,2,2,2,2,1},//14
                                          {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, //15
                                          {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1} //16
                                      };
    }
}
