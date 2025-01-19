using Runtime.Gameplay.Items;
using Runtime.Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Inventory.UI
{
    public class InventoryItemUIObject : MonoBehaviour , IPoolable
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amountText;
        
        public void Initialize(InventoryItem inventoryItem)
        {
            amountText.text = inventoryItem.amount.ToString();
            icon.sprite = ItemCatalog.Instance.GetItemInfo(inventoryItem.itemType).icon;
        }
        
        [field:SerializeField] public ObjectType ObjectType { get; set; }
        public void OnSpawn() { }
        public void OnReset() { }
    }
}