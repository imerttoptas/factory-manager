using UnityEngine;

namespace Runtime.Pooling
{
    public enum ObjectType
    {
        [InspectorName("UI/ItemUIObject")] ItemUIObject = 100,
        [InspectorName("UI/OrderUIObject")] OrderViewUIObject = 101,
        [InspectorName("UI/InventoryItemUIObject")] InventoryItemUIObject = 102,
        [InspectorName("UI/DemandInfoUIObject")] DemandInfoUIObject = 103
    }
}