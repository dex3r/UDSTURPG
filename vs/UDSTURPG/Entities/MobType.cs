using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;

namespace RPG.Entities
{
    public class MobType
    {
        public static readonly MobType MobMummy = new MobType(50, 10, 0.008f, MyTexture.MobMummy, EnumMobWalkingStyle.Stuttery, stepInterval: 25);

        private int hp;
        public int Hp
        {
            get { return hp; }
        }
        private int baseDmg;
        public int BaseDmg
        {
            get { return baseDmg; }
        }
        private float speed;
        public float Speed
        {
            get { return speed; }
        }
        private int stepInterval;
        /// <summary>
        /// Przerwa pomiędzy krokami (dla stylu Stuttery)
        /// </summary>
        public int StepInterval
        {
            get { return stepInterval; }
            set { stepInterval = value; }
        }

        private int stepLength;
        /// <summary>
        /// Długość jednego kroku
        /// </summary>
        public int StepLength
        {
            get { return stepLength; }
            set { stepLength = value; }
        }

        private MyTexture texture;
        public MyTexture Texture
        {
            get { return texture; }
        }

        private EnumMobWalkingStyle walkingStyle;
        public EnumMobWalkingStyle WalkingStyle
        {
            get { return walkingStyle; }
        }

        private MobType(int hp, int baseDmg, float speed, MyTexture texture, EnumMobWalkingStyle walkingStyle, int stepInterval = 0, int stepLength = 20)
        {
            this.hp = hp;
            this.baseDmg = baseDmg;
            this.speed = speed;
            this.texture = texture;
            this.walkingStyle = walkingStyle;
            this.stepInterval = stepInterval;
            this.stepLength = stepLength;
        }
    }
}
