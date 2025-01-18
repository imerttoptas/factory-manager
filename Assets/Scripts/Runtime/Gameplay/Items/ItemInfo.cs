using NaughtyAttributes;
using Runtime.Data;
using UnityEngine;

namespace Runtime.Gameplay.Items
{
    public class ItemInfo : ScriptableObject
    {
        [ShowAssetPreview] public Sprite icon;
        public ItemType itemType;
    }
}