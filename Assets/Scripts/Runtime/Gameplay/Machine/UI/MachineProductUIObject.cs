using System;
using System.Collections.Generic;
using Runtime.Data;
using Runtime.Data.Products;
using Runtime.Gameplay.Economy;
using Runtime.Gameplay.Inventory;
using Runtime.Pooling;
using Runtime.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.Gameplay.Machine.UI
{
    public class MachineProductUIObject : MonoBehaviour , IPoolable
    {
        #region Product Info

        [SerializeField] private ProductInfo productInfo;
        [SerializeField] private TextMeshProUGUI productNameText;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI batchSizeText;

        #endregion

        #region Purchase Amount Field

        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private Button increaseButton;
        [SerializeField] private Button decreaseButton;
        [SerializeField] private Button purchaseButton;
        
        #endregion
        

        public int amount;
        [SerializeField] private int capacity = 10;
        [SerializeField] private int overCapacity = 2;
        
        public void Initialize()
        {
            SetTexts();
            //batchSizeText.text = rawMaterialInfo.batchSize.ToString();
            //icon.sprite = rawMaterialInfo.icon;
            productNameText.text = productInfo.name;
            increaseButton.onClick.AddListener(IncreaseButtonAction);
            decreaseButton.onClick.AddListener(DecreaseButtonAction);
            purchaseButton.onClick.AddListener(ProduceButtonAction);
        }
        
        private void OnDisable()
        {
            increaseButton.onClick.RemoveAllListeners();
            decreaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.RemoveAllListeners();
        }

        private void IncreaseButtonAction()
        {
            if (amount < capacity + overCapacity)
            {
                amount += 1;
                SetTexts();
            }
        }

        private void DecreaseButtonAction()
        {
            if (amount > 0)
            {
                amount -= 1;
                SetTexts();
            }
        }

        private void SetTexts()
        {
            string amountStr = amount > capacity
                ? "<color=#FF0000>" + amount + "</color>/" + capacity
                : amount + "/" + capacity;
            amountText.text = amountStr;
            costText.text = (amount * productInfo.cost).ToString();
        }

        private void ProduceButtonAction()
        {
            if (CheckInventory(productInfo.requiredMaterialInfos) && GameEconomy.instance.DecreaseCoin(amount * productInfo.cost))
            {
                InventoryManager.instance.Inventory.IncreaseItemCount(productInfo.itemType, amount);
                foreach (var info in productInfo.requiredMaterialInfos)
                {
                    InventoryManager.instance.Inventory.DecreaseItemCount(info.materialInfo.itemType, info.amount);
                }
            }
        }

        private bool CheckInventory(List<RequiredMaterialInfo> requiredMaterialInfos)
        {
            foreach (var materialInfo in requiredMaterialInfos)
            {
                if (!InventoryManager.instance.IsExistInInventory(materialInfo.materialInfo.itemType,materialInfo.amount))
                {
                    WarningMessageText.Warning?.Invoke("Not Enough Raw Material!");
                    return false;
                }
            }
            return true;
        }

        [field:SerializeField] public ObjectType ObjectType { get; set; }
        public void OnSpawn() { }
        public void OnReset() { }
    }
}