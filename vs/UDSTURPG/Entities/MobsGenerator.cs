using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Controls;
using RPG.Main;
using RPG.Worlds;

namespace RPG.Entities
{
    public static class MobsGenerator
    {
        public const int TIME = 60;
        private static int time;

        //!? Properties region
        #region PROPERTIES
        public static int Time
        {
            get { return time; }
            set { time = value; }
        }
        #endregion
        //!? END of properties region

        public static void Update()
        {
            time++;
            var temp = (GameMain.CurrentPlayer.Score / 1000) + 1;
            if (time >= TIME / temp)
            {
                Random random = new Random();
                float y = (float)random.NextDouble() * (Chunk.CHUNK_SIZE_Y) - 1;
                addmob(Chunk.CHUNK_SIZE_X, y, random.Next(0, 4));
                time = 0;
            }
        }

        private static void addmob(float x, float y, int type)
        {
            EntityMob mob = null;
            if (type == 0)
            {
                mob = new EntityMob(x, y, MobType.MobMummy);
            }
            else if (type == 1)
            {
                mob = new EntityMob(x, y, MobType.MobSnake);
            }
            else if (type == 2)
            {
                mob = new EntityMob(x, y, MobType.MobScarab);
            }
            else if (type == 3)
            {
                mob = new EntityMob(x, y, MobType.MobBat);
            }
            GameMain.CurrentWorld.AddEntity(mob);
        }
    }
}
