using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Runtime.Data
{
    [CreateAssetMenu(fileName = "ProductInfo", menuName = "Gameplay/ProductInfo")]
    public class ProductInfo : ScriptableObject
    {
        [ShowAssetPreview] public Sprite icon;
        public List<RequiredMaterialInfo> requiredMaterialInfos;
        public ItemType itemType;
    }

    [System.Serializable]
    public class RequiredMaterialInfo
    {
        public RawMaterialInfo materialInfo;
        public int amount;
    }
}

