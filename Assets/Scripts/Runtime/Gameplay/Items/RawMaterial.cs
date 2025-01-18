using Runtime.Data;
using Runtime.Data.RawMaterials;
using UnityEngine;

namespace Runtime.Gameplay.Items
{
    public class RawMaterial : Item
    {
        [field: SerializeField] public RawMaterialInfo rawMaterialInfo;
    }
}
