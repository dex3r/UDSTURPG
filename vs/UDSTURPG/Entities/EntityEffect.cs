using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.Textures2D;

namespace RPG.Entities
{
    public class EntityEffect : Entity
    {
        #region properites

        #endregion

        public EntityEffect(float posX, float posY, MyTexture texture)
            : base(posX, posY)
        {
            this.currentTexture = texture;
            this.IsColidable = false;
        }

        public override void Draw()
        {
            base.Draw();
            if(animationFrame >= (currentTexture.FramesCount * currentTexture.AnimationSpeed) - 1)
            {
                MarkedToDelete = true;
            }
        }
    }
}
