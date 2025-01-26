using Runtime.Data.RawMaterials;
using Runtime.Gameplay.Economy;
using Runtime.Gameplay.Inventory;
using Runtime.Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Supplier.UI
{
    public class SupplierItemUIObject : MonoBehaviour , IPoolable
    {
        [SerializeField] private SupplyOrderInfo info;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI defectRateText;
        [SerializeField] private TextMeshProUGUI batchSizeText;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private Button increaseButton;
        [SerializeField] private Button decreaseButton;
        [SerializeField] private Button purchaseButton;
        public int amount;
        
        public void Initialize()
        {
            SetTexts();
            batchSizeText.text = info.batchSize.ToString();
            icon.sprite = info.rawMaterialInfo.icon;
            defectRateText.text = "Defect Rate " + info.defectRate + "%";
            increaseButton.onClick.AddListener(IncreaseButtonAction);
            decreaseButton.onClick.AddListener(DecreaseButtonAction);
            purchaseButton.onClick.AddListener(PurchaseButtonAction);
        }
        
        private void OnDisable()
        {
            increaseButton.onClick.RemoveAllListeners();
            decreaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.RemoveAllListeners();
        }

        private void IncreaseButtonAction()
        {
            if (amount < info.capacity)
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
            timerText.text = info.leadTime + "days";
            amountText.text = amount.ToString();
            costText.text = (amount * info.batchCost).ToString();
        }

        private void PurchaseButtonAction()
        {
            if (GameEconomy.instance.DecreaseCoin(amount * info.batchCost))
            {
                GameManager.instance.AddToWaitingCommands(new GameTimeCommand(GameManager.instance.GameTime.AddDays(info.leadTime),
                    () =>
                    {
                        InventoryManager.instance.Inventory.IncreaseItemCount(info.rawMaterialInfo.itemType,
                            amount * info.batchSize);
                    }));
    
            }
        }
        
        [field:SerializeField] public ObjectType ObjectType { get; set; }
        public void OnSpawn() { }
        public void OnReset() { }
    }
}