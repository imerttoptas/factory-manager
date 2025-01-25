using NaughtyAttributes;
using Runtime.Data;
using Runtime.Gameplay.Inventory.UI;
using UnityEngine;

namespace Runtime.Gameplay.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private Inventory inventory;
        public Inventory Inventory
        {
            get
            {
                if (inventory != null)
                {
                    return inventory;
                }

                inventory = new Inventory();
                return inventory;
            }
        }
        [field: SerializeField] private InventoryPanel inventoryPanelPrefab;
        private InventoryPanel inventoryPanel;
        public InventoryPanel InventoryPanel
        {
            get
            {
                if (!inventoryPanel)
                {
                    inventoryPanel = Instantiate(inventoryPanelPrefab, GameManager.instance.mainCanvas);
                }
                
                return inventoryPanel;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            IncreaseCapacity(100);
            IncreaseItemCount(ItemType.Wood, 15);
            IncreaseItemCount(ItemType.Iron, 15);
            IncreaseItemCount(ItemType.Nail, 15);
        }

        public void IncreaseCapacity(int amount)
        {
            Inventory.capacity += amount;
        }
        
        public void DecreaseItemCount(ItemType itemType, int count)
        {
            var targetItem = Inventory.inventoryItems.Find(x => x.itemType == itemType);
            if (targetItem != null)
            {
                targetItem.amount -= count;
            }
        }        
        
        public void IncreaseItemCount(ItemType itemType, int count)
        {
            var targetItem = Inventory.inventoryItems.Find(x => x.itemType == itemType);
            if (targetItem != null)
            {
                targetItem.amount += count;
            }
            else
            {
                Inventory.inventoryItems.Add(new InventoryItem(itemType, count));
            }
        }

        public bool IsExistInInventory(ItemType itemType, int count)
        {
            var targetItem = Inventory.inventoryItems.Find(x => x.itemType == itemType);
            if (targetItem != null)
            {
                return targetItem.amount >= count;
            }
            
            return false;
        }

#if UNITY_EDITOR
        [SerializeField] private ItemType type;
        [SerializeField] private int amount;
        [Button]
        public void IncreaseInventoryItem()
        {
            IncreaseItemCount(type, amount);
        }
#endif
    }
}