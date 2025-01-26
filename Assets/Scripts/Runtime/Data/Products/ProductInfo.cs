using System.Collections.Generic;
using Runtime.Data.RawMaterials;
using Runtime.Gameplay.Items;
using UnityEngine;

namespace Runtime.Data.Products
{
    [CreateAssetMenu(fileName = "ProductInfo", menuName = "Gameplay/ProductInfo")]
    public class ProductInfo : ItemInfo
    {
        public List<RequiredMaterialInfo> requiredMaterialInfos;
        public int cost;
        public string name;
    }

    [System.Serializable]
    public class RequiredMaterialInfo
    {
        public RawMaterialInfo materialInfo;
        public int amount;
    }
}

