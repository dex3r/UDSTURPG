using Microsoft.Xna.Framework;
using RPG.Main;
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

        private static float[] coinsSize = { 0.25f, 0.25f, 0.255f, 0.5f, 0.5f, 0.5f, 0.375f, 0.375f, 0.75f };

        private static int[] coinsValues = { 1, 2, 5, 10, 20, 50, 100, 200, 500 };

        private int timeoutTimer;

        private bool pullMoney;
        private int coinValue;

        #region properties
        public static MyTexture[] CoinsTextures
        {
            get { return EntityCoin.coinsTextures; }
        }
        public static int[] CoinsValues
        {
            get { return EntityCoin.coinsValues; }
        }
        public static float[] CoinsSize
        {
            get { return EntityCoin.coinsSize; }
        }
        public bool PullMoney
        {
            get { return pullMoney; }
            set { pullMoney = value; }
        }
        public int CoinValue
        {
            get { return coinValue; }
        }

        #endregion

        public EntityCoin(float posX, float posY, int coinValue)
            : base(posX, posY)
        {
            this.coinValue = coinValue;
            currentTexture = CoinsTextures[coinValue];
            SetCollisionBox(0, 0, coinsSize[coinValue], coinsSize[coinValue]);
            coinValue = CoinsValues[coinValue];
            Rotation = (float)(random.NextDouble());
            HitRecoil = (float)(random.Next(30,50)/100f);
            timeoutTimer = 15 * 60;
            IsAnimatedOnlyOnMove = false;
        }

        public override void Update()
        {
            timeoutTimer--;
            if (timeoutTimer <= 0)
            {
                MarkedToDelete = true;
            }
            if (pullMoney)
            {
                Vector2 interp = Vector2.Subtract(new Vector2((PosX + coinsSize[coinValue]) * 64, (PosY + coinsSize[coinValue]) * 64), new Vector2((GameMain.CurrentPlayer.PosX + GameMain.CurrentPlayer.CollisionBoxX + GameMain.CurrentPlayer.CollisionBoxWidth / 2) * 64, (GameMain.CurrentPlayer.PosY + GameMain.CurrentPlayer.CollisionBoxY + GameMain.CurrentPlayer.CollisionBoxHeight / 2) * 64));
                interp.Normalize();
                interp = Vector2.Multiply(interp, (float)Math.PI);
                Rotation = Math.Atan2(-interp.Y, -interp.X);
                currentVelocity = (float)(Math.Pow(Distance((Entity)GameMain.CurrentPlayer),-1)/10);
            }
            else
            {
                //HitRecoil = currentVelocity;
            }
            base.Update();
        }
    }
}
