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
        private int chunkNumbers;
        private int chunksInRow;
        private Chunk[] chunks;
        private List<Entity> entities;
        private List<Entity> entitiesToAdd;

        //!? Properties region
        #region PROPERTIES
        public int Size
        {
            get { return size; }
        }
        public List<Entity> Entities
        {
            get { return entities; }
        }
        /// <summary>
        /// Ilość wszystkich chunków dla obecnej wielkości świata
        /// </summary>
        public int ChunkNumbers
        {
            get { return chunkNumbers; }
            set { chunkNumbers = value; }
        }
        /// <summary>
        /// Ilość chunków w jednym rzędzie (pierwiastek z ChunkNumbers)
        /// </summary>
        public int ChunksInRow
        {
            get { return chunksInRow; }
            set { chunksInRow = value; }
        }
        #endregion
        //!? END of properties region

        public World()
        {
            size = 16;
            //ChunkNumbers = (int)Math.Pow(size / Chunk.CHUNK_SIZE, 2);
            //ChunksInRow = size / Chunk.CHUNK_SIZE;
            ChunkNumbers = 1;
            ChunksInRow = 1;
            chunks = new Chunk[ChunkNumbers];
            for (int x = 0; x < ChunksInRow; x++)
            {
                for (int y = 0; y < ChunksInRow; y++)
                {
                    chunks[ChunksInRow * y + x] = new Chunk(this, x, y);
                }
            }
            entities = new List<Entity>();
            entitiesToAdd = new List<Entity>();
        }

        public Chunk GetChunk(int x, int y)
        {
            return chunks[ChunksInRow * y + x];
        }

        public void Update()
        {
            List<Entity> entitiesToDelete = new List<Entity>();
            foreach (Entity entity in entities)
            {
                entity.Update();
                if (entity.MarkedToDelete)
                {
                    entitiesToDelete.Add(entity);
                }
                if (entity.MarkedToDeleteInNextTick)
                {
                    entity.MarkedToDelete = true;
                }
            }
            foreach (Entity entity in entitiesToDelete)
            {
                entities.Remove(entity);
            }
            entities.AddRange(entitiesToAdd);
            entitiesToAdd.Clear();
        }

        public void AddEntity(Entity entity)
        {
            entitiesToAdd.Add(entity);
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
