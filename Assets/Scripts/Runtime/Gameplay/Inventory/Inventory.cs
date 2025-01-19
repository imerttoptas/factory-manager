using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Runtime.Data;

namespace Runtime.Gameplay.Inventory
{
    [Serializable]
    public class Inventory
    {
        public int capacity;
        public List<InventoryItem> inventoryItems = new();
        [JsonIgnore] public int CurrentItemCount
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
