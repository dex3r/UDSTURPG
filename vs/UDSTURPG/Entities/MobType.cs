using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;

namespace RPG.Entities
{
    public class MobType
    {
        public static readonly MobType MobMummy = new MobType(50, 10, 0.02f, MyTexture.MobMummy, EnumMobWalkingStyle.Stuttery, 0.15f, 0.2f, 1f, 1f, stepInterval: 25);
        public static readonly MobType MobSnake = new MobType(30, 20, 0.05f, MyTexture.MobSnake, EnumMobWalkingStyle.Stuttery, 0.25f, 0.35f, 0.95f, 0.75f, stepInterval: 25, stepLength: 40);
        public static readonly MobType MobScarab = new MobType(80, 15, 0.023f, MyTexture.MobScarab, EnumMobWalkingStyle.Stuttery, 0.25f, 0.35f, 0.95f, 0.75f, stepInterval: 25, stepLength: 40);

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

        private float collisionBoxWidth;

        public float CollisionBoxWidth
        {
            get { return collisionBoxWidth; }
        }
        private float collisionBoxHeight;

        public float CollisionBoxHeight
        {
            get { return collisionBoxHeight; }
        }

        private float collisionBoxX;
        public float CollisionBoxX
        {
            get { return collisionBoxX; }
        }
        private float collisionBoxY;
        public float CollisionBoxY
        {
            get { return collisionBoxY; }
        }

        private MobType(int hp, int baseDmg, float speed, MyTexture texture, EnumMobWalkingStyle walkingStyle, float collisionBoxX, float collisionBoxY, float collisionBoxWidth, float collisionBoxHeight, int stepInterval = 0, int stepLength = 20)
        {
            this.hp = hp;
            this.baseDmg = baseDmg;
            this.speed = speed;
            this.texture = texture;
            this.walkingStyle = walkingStyle;
            this.stepInterval = stepInterval;
            this.stepLength = stepLength;
            this.collisionBoxHeight = collisionBoxHeight;
            this.collisionBoxWidth = collisionBoxWidth;
            this.collisionBoxX = collisionBoxX;
            this.collisionBoxY = collisionBoxY;
        }
    }
}
