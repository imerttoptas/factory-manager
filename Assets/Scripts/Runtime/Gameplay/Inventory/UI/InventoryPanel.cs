using System.Collections.Generic;
using Runtime.Pooling;
using Runtime.UI;
using TMPro;
using UnityEngine;

namespace Runtime.Gameplay.Inventory.UI
{
    public class InventoryPanel : Panel
    {
        [SerializeField] private Transform contentParent;
        [SerializeField] private TextMeshProUGUI capacityIndicatorText;
        private List<InventoryItemUIObject> inventoryItemUIObjects = new();
        public override void Open()
        {
            base.Open();
            cancelButton.onClick.AddListener(Close);
            InitializeCapacityText();
            InitializeInventoryItemUIObjects();
        }

        private void InitializeCapacityText()
        {
            capacityIndicatorText.text = InventoryManager.instance.Inventory.CurrentItemCount + "/" +
                                         InventoryManager.instance.Inventory.capacity;
        }

        private void InitializeInventoryItemUIObjects()
        {
            foreach (var inventoryItem in InventoryManager.instance.Inventory.inventoryItems)
            {
                var inventoryItemUIObject =
                    PoolFactory.instance.GetObject<InventoryItemUIObject>(ObjectType.InventoryItemUIObject,
                        contentParent);
                inventoryItemUIObject.Initialize(inventoryItem);
                inventoryItemUIObjects.Add(inventoryItemUIObject);
            }
        }

        public override void Close()
        {
            RemoveAllListeners();
            foreach (var inventoryItem in inventoryItemUIObjects)
            {
                inventoryItem.ResetObject();
            }
            inventoryItemUIObjects.Clear();
            base.Close();
        }
    }
}
