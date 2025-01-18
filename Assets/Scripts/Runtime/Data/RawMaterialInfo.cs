using NaughtyAttributes;
using UnityEngine;

namespace Runtime.Data
{
    [CreateAssetMenu(fileName = "RawMaterialInfo",menuName = "Gameplay/RawMaterialInfo")]
    public class RawMaterialInfo : ScriptableObject
    {
        [ShowAssetPreview] public Sprite icon;
        public ItemType itemType;
        public int batchSize;
    }
}
