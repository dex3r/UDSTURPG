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
        private int coinIndex;


        //!? Properties region
        #region PROPERTIES
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
        public int CoinIndex
        {
            get { return coinIndex; }
            set { coinIndex = value; }
        }
        #endregion
        //!? END of properties region

        public static void Loot(float posX, float posY, int money)
        {
            int coinsCount;
            if(money < 50)
            {
                coinsCount = Entity.random.Next(2, 4);
            }
            else if (money < 100)
            {
                coinsCount = Entity.random.Next(2, 5);
            }
            else
            {
                coinsCount = Entity.random.Next(2, 6);
            }
            int moneyLeft = money;
            int rand;
            int[] coinsWorth = new int[coinsCount];
            for(int i = 0; i < coinsWorth.Length - 1; i++)
            {
                rand = Entity.random.Next(1, Math.Max(1, Math.Min(moneyLeft - coinsWorth.Length, 501)));
                moneyLeft -= rand;
                coinsWorth[i] = rand;
            }
            coinsWorth[coinsWorth.Length - 1] = moneyLeft;
            for(int i = 0; i < coinsWorth.Length; i++)
            {
                int coinsValuesIndex = coinsValues.Length - 1;
                for(int j = 1; j < coinsValues.Length; j++)
                {
                    if(coinsValues[j] > coinsWorth[i])
                    {
                        coinsValuesIndex = j - 1;
                        break;
                    }
                }
                EntityCoin coin = new EntityCoin(posX, posY, coinsWorth[i], coinsValuesIndex);
                GameMain.CurrentWorld.AddEntity(coin);
            }
        }

        public EntityCoin(float posX, float posY, int coinValue, int coinIndex)
            : base(posX, posY)
        {
            this.coinValue = coinValue;
            this.coinIndex = coinIndex;
            currentTexture = CoinsTextures[coinIndex];
            SetCollisionBox(0, 0, coinsSize[coinIndex], coinsSize[coinIndex]);
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
                Vector2 interp = Vector2.Subtract(new Vector2((PosX + coinsSize[coinIndex]) * 64, (PosY + coinsSize[coinIndex]) * 64), new Vector2((GameMain.CurrentPlayer.PosX + GameMain.CurrentPlayer.CollisionBoxX + GameMain.CurrentPlayer.CollisionBoxWidth / 2) * 64, (GameMain.CurrentPlayer.PosY + GameMain.CurrentPlayer.CollisionBoxY + GameMain.CurrentPlayer.CollisionBoxHeight / 2) * 64));
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
