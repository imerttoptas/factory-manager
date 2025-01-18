using UnityEngine;

namespace Runtime.Pooling
{
    public class PoolObject : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public ObjectType ObjectType { get; set; }
        
        public virtual void OnSpawn() { }

        public virtual void OnReset() { }

        public void OnParticleSystemStopped()
        {
            if (gameObject.activeInHierarchy)
            {
                this.ResetObject();
            }
        }
    }
}