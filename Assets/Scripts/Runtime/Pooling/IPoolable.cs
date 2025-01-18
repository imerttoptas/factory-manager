using UnityEngine;

namespace Runtime.Pooling
{
	public interface IPoolable
	{
		public ObjectType ObjectType { get; set; }
		GameObject GameObject => ((MonoBehaviour)this).gameObject;

		void OnSpawn();
		void OnReset();
	}
	
	public static class IPoolableExtensions
	{
		public static bool ResetObject(this IPoolable poolable, bool removeFromActiveObjects=true)
		{
			return PoolFactory.instance.ResetObject(poolable,removeFromActiveObjects);
		}
	}
}