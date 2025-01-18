using System;
using System.Collections.Generic;
using Runtime.Data;

namespace Runtime.Gameplay.Inventory
{
    [Serializable]
    public class Inventory
    {
        public int capacity;
        private List<InventoryItem> inventoryItems;
        public int CurrentItemCount
        {
            get
            {
                var totalCount = 0;
                foreach (var inventoryItem in inventoryItems)
                {
                    totalCount += inventoryItem.amount;
                }

                return totalCount;
            }
        }

        public Inventory()
        {
            
        }
        
        public void IncreaseCapacity(int amount)
        {
            capacity += amount;
        }
        
        public void DecreaseItemCount(ItemType itemType, int count)
        {
            var targetItem = inventoryItems.Find(x => x.itemType == itemType);
            if (targetItem != null)
            {
                targetItem.amount -= count;
            }
        }        
        
        public void IncreaseItemCount(ItemType itemType, int count)
        {
            var targetItem = inventoryItems.Find(x => x.itemType == itemType);
            if (targetItem != null)
            {
                targetItem.amount += count;
            }
            else
            {
                inventoryItems.Add(new InventoryItem(itemType, count));
            }
        }
    }

    [Serializable]
    public class InventoryItem
    {
        public ItemType itemType;
        public int amount;

        public InventoryItem(ItemType itemType, int amount)
        {
            this.itemType = itemType;
            this.amount = amount;
        }
    }
}
