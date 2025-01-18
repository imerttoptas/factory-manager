using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Pooling
{
    public class PoolFactory : Singleton<PoolFactory>
    {
        private Dictionary<ObjectType, ObjectPool> pools;
        
        [SerializeField] private PoolInfo[] poolInfoList; 
        [SerializeField] private GameObject[] prefabs; 
        
        protected override void Awake()
        {
            base.Awake();

            if (instance == this)
            {
                Initialize();
            }
        }
        
        private void Initialize()
        {
            if (pools == null)
            {
                pools = new Dictionary<ObjectType, ObjectPool>();

                foreach (GameObject obj in prefabs)
                {
                    CreateObjectPool(obj);
                }
            }
        }
        
        private void CreateObjectPool(GameObject obj)
        {
            IPoolable poolableObj = TryToGetIPoolable(obj);
            if (poolableObj != null && !pools.ContainsKey(poolableObj.ObjectType))
            {
                ObjectPool objectPool = new ObjectPool(poolableObj);
                pools.Add(poolableObj.ObjectType, objectPool);
            }
            
            IPoolable TryToGetIPoolable(GameObject targetObj)
            {
                foreach (MonoBehaviour monoBehaviour in targetObj.GetComponents<MonoBehaviour>())
                {
                    if (monoBehaviour is IPoolable iPoolable)
                    {
                        return iPoolable;
                    }
                }

                Debug.LogError((targetObj.name, "is not an IPoolable object."));
                return null;
            }
        }
        
        #region Get Methods

        private ObjectPool GetPool(ObjectType type)
        {
            return pools.GetValueOrDefault(type);
        }
        
        public T GetObject<T>(ObjectType type, Transform parent = null, Vector3? position = null, Vector3? scale = null)
            where T : IPoolable
        {
            return (T)GetObject(type, parent, position, scale);
        }

        private IPoolable GetObject(ObjectType type, Transform parent = null, Vector3? position = null, Vector3? scale = null)
        {
            ObjectPool pool = GetPool(type);
            return pool?.Pull(parent, position, scale);
        }
        
        public ParticleSystem GetParticleEffect(ObjectType type, Vector2 position, Transform parent = null)
        {
            ParticleSystem particle = GetObject(type, parent, position, Vector3.one).GameObject.GetComponent<ParticleSystem>();
            particle.Play(true);
            
            var main = particle.main;
            main.stopAction = ParticleSystemStopAction.Callback;
            
            return particle;
        }
        
        #endregion

        #region Reset Methods
        public void ResetPool(ObjectType objectType)
        {
            GetPool(objectType).Reset();
        }
        
        public bool ResetObject(IPoolable obj, bool removeFromActiveObjects=true)
        {
            ObjectPool pool = GetPool(obj.ObjectType);
            if (pool != null)
            {
                pool.Push(obj,removeFromActiveObjects);
                return true;
            }
            Debug.LogError(("The object you are trying to reset doesn't exist in any pool ->", obj.ObjectType));
            return false;
        }

        public void ResetAll(params ObjectType[] excludedTypes)
        {
            foreach (KeyValuePair<ObjectType, ObjectPool> pool in pools)
            {
                if (excludedTypes?.Length > 0 && Array.IndexOf(excludedTypes, pool.Key) > -1)
                {
                    continue;
                }
                pool.Value.Reset();
            }
        }
        
        #endregion
    }
    
    [Serializable]
    public class PoolInfo
    {
        public GameObject obj;
        public int initialSize;
    }
}