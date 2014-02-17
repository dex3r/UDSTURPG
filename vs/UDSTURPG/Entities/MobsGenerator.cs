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
        public static int Time
        {
            get { return time; }
            set { time = value; }
        }
        public static void Update()
        {
            time++;
            var temp = (GameMain.CurrentPlayer.Score / 10)+1;
            if (time >= TIME/temp)
            {
                Random random = new Random();
                float y = (float)random.NextDouble() * (Chunk.CHUNK_SIZE_Y) - 1;
                addmob(Chunk.CHUNK_SIZE_X,y,random.Next(0,3));
                time = 0;
            }
        }

        private static void addmob(float x, float y, int type)
        {
            if (type == 0)
            {
                EntityMob mummy = new EntityMob(x, y, MobType.MobMummy);
                GameMain.CurrentWorld.AddEntity(mummy);
            }
            else if(type == 1)
            {
                EntityMob snake = new EntityMob(x, y, MobType.MobSnake);
                GameMain.CurrentWorld.AddEntity(snake);
            }
            else if (type == 2)
            {
                EntityMob scarab = new EntityMob(x, y, MobType.MobScarab);
                GameMain.CurrentWorld.AddEntity(scarab);
            }
        }
    }
}
