using UnityEngine;

namespace Runtime.Data.RawMaterials
{
    [CreateAssetMenu(fileName = "SupplyOrderInfo",menuName = "Gameplay/SupplyOrderInfo")]
    public class SupplyOrderInfo : ScriptableObject
    {
        public RawMaterialInfo rawMaterialInfo;
        public int batchSize;
        public int batchCost;
        public int leadTime;
        public int capacity;
        public int defectRate;
    }
}
