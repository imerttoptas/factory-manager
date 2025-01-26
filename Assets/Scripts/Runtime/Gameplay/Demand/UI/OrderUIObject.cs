using Runtime.Data;
using Runtime.Data.Demand;
using Runtime.Gameplay.Inventory;
using Runtime.Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Demand.UI
{
    public class OrderUIViewObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amountText;
        
        public void Initialize(OrderInfo info)
        {
            amountText.text = InventoryManager.instance.GetInventoryItemCount(info.productInfo.itemType) + "/" +
                              info.quantity;
            icon.sprite = info.productInfo.icon;
        }
        [field: SerializeField] public ObjectType ObjectType { get; set; }
        public void OnSpawn() { }
        public void OnReset() { }
    }
}