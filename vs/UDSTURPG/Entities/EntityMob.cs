using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;
using RPG.Main;
using Microsoft.Xna.Framework;

namespace RPG.Entities
{
    public class EntityMob : EntityLiving
    {
        private int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        private EnumSheetNormalMob movementTextureState;
        public EnumSheetNormalMob MovementTextureState
        {
            get { return movementTextureState; }
            set { movementTextureState = value; }
        }

        private MobType mobType;
        public MobType MobType
        {
            get { return mobType; }
        }

        private int stepLength;
        /// <summary>
        /// Długość kroku (w tickach)
        /// </summary>
        public int StepLength
        {
            get { return stepLength; }
        }

        public EntityMob(float posX, float posY, MobType mobType, ulong id)
            : base(posX, posY, id)
        {
            this.mobType = mobType;
            this.CurrentHp = this.MaxHp = mobType.Hp;
            this.maxSpeed = mobType.Speed;
            this.damage = mobType.BaseDmg;
            this.currentTexture = mobType.Texture;
            movementTextureState = EnumSheetNormalMob.Down;
            currentVelocity = maxSpeed;
            rotation = -(3.0d * Math.PI) / 2.0d;
            this.IsColidable = true;
            AutoColisionBox(); //TEMP
        }

        public override void Update()
        {
            base.Update();
            if (mobType.StepInterval != 0)
            {
                stepLength++;
                if (currentVelocity > 0 && stepLength >= mobType.StepLength)
                {
                    stepLength = 0;
                    currentVelocity = 0;
                }
                else if (currentVelocity == 0 && stepLength >= mobType.StepInterval)
                {
                    stepLength = 0;
                    currentVelocity = maxSpeed;
                }
            }

            foreach(Entity en in GameMain.CurrentWorld.Entities)
                if (en is EntityBullet)
                {
                    if(Collision(en))
                    {
                        marketToDelete = true;
                    }
                }
            
        }

        public override Rectangle GetCurrentSourceRectangle()
        {
            return currentTexture.GetCurrentSourceRectangle(animationFrame, (int)MovementTextureState);
        }
    }
}
