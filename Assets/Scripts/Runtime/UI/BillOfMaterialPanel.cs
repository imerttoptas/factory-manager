using Runtime.Pooling;
using UnityEngine;

namespace UI
{
    public class BillOfMaterialPanel : MonoBehaviour , IPoolable
    {
        public void Initialize()
        {
            
        }

        [field: SerializeField] public ObjectType ObjectType { get; set; }

        public void OnSpawn()
        {
            throw new System.NotImplementedException();
        }

        public void OnReset()
        {
            throw new System.NotImplementedException();
        }
    }
}
