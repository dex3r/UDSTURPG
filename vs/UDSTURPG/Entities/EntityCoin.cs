using RPG.Textures2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG.Entities
{
    public class EntityCoin : EntityMovable
    {
        private static MyTexture[] coinsTextures = {
		MyTexture.CoinBronzeSmall,
		MyTexture.CoinSilverSmall,
		MyTexture.CoinGoldSmall,
		MyTexture.CoinBronze,
		MyTexture.CoinSilver,
		MyTexture.CoinGold,
		MyTexture.GemEmerald,
		MyTexture.GemRuby,
		MyTexture.GemDiamond };

        private static int[] coinsValues = { 1, 2, 5, 10, 20, 50, 100, 200, 500 };

        #region properties
        public static MyTexture[] CoinsTextures
        {
            get { return EntityCoin.coinsTextures; }
        }
        public static int[] CoinsValues
        {
            get { return EntityCoin.coinsValues; }
        }
        #endregion

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
