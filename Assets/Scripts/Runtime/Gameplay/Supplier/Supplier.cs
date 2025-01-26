using System.Collections.Generic;
using Runtime.Gameplay.Input;
using Runtime.Gameplay.Inventory;
using Runtime.Gameplay.Items;
using UnityEngine;

namespace Runtime.Gameplay.Supplier
{
    public class Supplier : MonoBehaviour , IClickableObject
    {
        public List<SupplyItem> supplyItems;
        public void PurchaseSupplyItem(SupplyItem supplyItem)
        {
            InventoryManager.instance.IncreaseItemCount(supplyItem.itemInfo.itemType, supplyItem.batchSize);
        }

        public void OnClick()
        {
            
        }
        
        public bool CanBeClicked { get; set; }
        public GameObject GameObject => gameObject;
    }

    [System.Serializable]
    public class SupplyItem
    {
        public ItemInfo itemInfo;
        public int batchSize;
    }
}