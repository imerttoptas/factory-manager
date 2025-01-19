using System.Collections.Generic;
using Runtime.Data;
using UnityEngine;

namespace Runtime.Gameplay.Items
{
    [CreateAssetMenu(fileName = "ItemCatalog", menuName = "Gameplay/ItemCatalog")]
    public class ItemCatalog : SingletonScriptableObject<ItemCatalog>
    {
        public List<ItemInfo> itemInfos;

        public ItemInfo GetItemInfo(ItemType type)
        {
            return itemInfos.Find(x => x.itemType == type);
        }
    }
}