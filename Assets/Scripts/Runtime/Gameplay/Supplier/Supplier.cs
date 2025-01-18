using System.Collections.Generic;
using Runtime.Gameplay.Inventory;
using Runtime.Gameplay.Items;
using UnityEngine;

namespace Runtime.Gameplay.Supplier
{
    public class Supplier : MonoBehaviour
    {
        public List<SupplyItem> supplyItems;

        public void PurchaseSupplyItem(SupplyItem supplyItem)
        {
            InventoryManager.instance.Inventory.IncreaseItemCount(supplyItem.itemInfo.itemType, supplyItem.batchSize);
        }
    }

    [System.Serializable]
    public class SupplyItem
    {
        public ItemInfo itemInfo;
        public int batchSize;
        
        
    }
}