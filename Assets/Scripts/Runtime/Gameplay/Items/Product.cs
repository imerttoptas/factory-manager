using Runtime.Data;
using Runtime.Data.Products;
using UnityEngine;

namespace Runtime.Gameplay.Items
{
    public class Product : Item
    {
        [field:SerializeField] public ProductInfo productInfo;
    }
}
