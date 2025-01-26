using System;
using NaughtyAttributes;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Gameplay.Economy
{
    public class GameEconomy : Singleton<GameEconomy>
    {
        public Action<int> OnCoinCountChanged;
        private int coin;
        public int Coin
        {
            get => coin;
            private set => coin = value;
        }

        [SerializeField] private int initialCoin;
        private void Start()
        {
            IncreaseCoin(initialCoin);
        }

        public void IncreaseCoin(int amount)
        {
            if (amount > 0)
            {
                Coin += amount;
                OnCoinCountChanged?.Invoke(Coin);
            }
        }
        
        public bool DecreaseCoin(int amount)
        {
            if (Coin - amount >= 0)
            {
                Coin -= amount;
                OnCoinCountChanged?.Invoke(Coin);
                return true;
            }
            WarningMessageText.Warning?.Invoke("Not Enough Coin!");
            return false;
        }

#if UNITY_EDITOR
        [SerializeField] private int increaseAmount;
        [Button]
        public void IncreaseCoinEditor()
        {
            IncreaseCoin(increaseAmount);
        }
#endif
    }
}