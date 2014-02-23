using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;

namespace RPG.Entities
{
    public class MobType
    {
        public static readonly MobType MobMummy = new MobType(50, 10, 0.02f, MyTexture.MobMummy, EnumMobWalkingStyle.Stuttery, 0.15f, 0.2f, 1f, 1f, stepInterval: 25, worth: 10);
        public static readonly MobType MobSnake = new MobType(30, 20, 0.05f, MyTexture.MobSnake, EnumMobWalkingStyle.Stuttery, 0.25f, 0.35f, 0.95f, 0.75f, stepInterval: 25, stepLength: 40, worth: 6);
        public static readonly MobType MobScarab = new MobType(45, 15, 0.023f, MyTexture.MobScarab, EnumMobWalkingStyle.Stuttery, 0.25f, 0.35f, 0.95f, 0.75f, stepInterval: 25, stepLength: 40, worth: 8);
        public static readonly MobType MobBat = new MobType(20, 5, 0.04f, MyTexture.MobBat, EnumMobWalkingStyle.Flying, 0.1f, 0.2f, 0.85f, 0.7f, worth: 5);

        private int hp;
        private int baseDmg;
        private float speed;
        private int stepInterval;
        private int stepLength;
        private MyTexture texture;
        private EnumMobWalkingStyle walkingStyle;
        private float collisionBoxWidth;
        private float collisionBoxHeight;
        private float collisionBoxX;
        private float collisionBoxY;
        private int worth;

        //!? Properties region
        #region PROPERTIES
        public int Hp
        {
            get { return hp; }
        }
        public int BaseDmg
        {
            get { return baseDmg; }
        }
        public float Speed
        {
            get { return speed; }
        }
        /// <summary>
        /// Przerwa pomiędzy krokami (dla stylu Stuttery)
        /// </summary>
        public int StepInterval
        {
            get { return stepInterval; }
            set { stepInterval = value; }
        }
        /// <summary>
        /// Długość jednego kroku
        /// </summary>
        public int StepLength
        {
            get { return stepLength; }
            set { stepLength = value; }
        }
        public MyTexture Texture
        {
            get { return texture; }
        }
        public EnumMobWalkingStyle WalkingStyle
        {
            get { return walkingStyle; }
        }
        public float CollisionBoxWidth
        {
            get { return collisionBoxWidth; }
        }
        public float CollisionBoxHeight
        {
            get { return collisionBoxHeight; }
        }
        public float CollisionBoxX
        {
            get { return collisionBoxX; }
        }
        public float CollisionBoxY
        {
            get { return collisionBoxY; }
        }
        public int Worth
        {
            get { return worth; }
        }
        #endregion
        //!? END of properties region

        private MobType(int hp, int baseDmg, float speed, MyTexture texture, EnumMobWalkingStyle walkingStyle, float collisionBoxX, float collisionBoxY, float collisionBoxWidth, float collisionBoxHeight, int worth, int stepInterval = 0, int stepLength = 20)
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
            this.worth = worth;
        }
    }
}
