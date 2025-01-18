using System.Collections.Generic;
using Runtime.Gameplay.Items;
using UnityEngine;

namespace Runtime.Data
{
    [CreateAssetMenu(fileName = "ProductInfo", menuName = "Gameplay/ProductInfo")]
    public class ProductInfo : ItemInfo
    {
        public List<RequiredMaterialInfo> requiredMaterialInfos;
    }

    [System.Serializable]
    public class RequiredMaterialInfo
    {
        public RawMaterialInfo materialInfo;
        public int amount;
    }
}

