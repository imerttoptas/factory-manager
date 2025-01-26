using System;
using TMPro;
using UnityEngine;

namespace Runtime.Gameplay.Economy.UI
{
    public class CoinIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountText;
        public void Start()
        {
            SetText(GameEconomy.instance.Coin);
            GameEconomy.instance.OnCoinCountChanged += SetText;
        }

        private void OnDisable()
        {
            GameEconomy.instance.OnCoinCountChanged -= SetText;
        }

        private void SetText(int amount)
        {
            amountText.text = amount.ToString();
        }
    }
}
