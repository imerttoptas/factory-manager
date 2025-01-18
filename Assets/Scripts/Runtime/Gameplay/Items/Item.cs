using Runtime.Data;
using Runtime.Pooling;
using UnityEngine;

namespace Runtime.Gameplay.Items
{
    public abstract class Item : MonoBehaviour , IPoolable
    {
        public ItemType itemType;
        public ObjectType ObjectType { get; set; }
        public virtual void OnSpawn()
        {
        }

        public virtual void OnReset()
        {
        }
    }
}