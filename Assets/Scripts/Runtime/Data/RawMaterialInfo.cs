using Runtime.Gameplay.Items;
using UnityEngine;

namespace Runtime.Data
{
    [CreateAssetMenu(fileName = "RawMaterialInfo",menuName = "Gameplay/RawMaterialInfo")]
    public class RawMaterialInfo : ItemInfo
    {
        public int batchSize;
    }
}
