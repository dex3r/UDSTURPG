using RPG.Textures2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG.Entities
{
    public class EntityCoin : EntityMovable
    {
        public EntityCoin(float posX, float posY, MyTexture texture)
            : base(posX, posY)
        {
            
        }
        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
