using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T instance;
    public bool isPersistent = true;
    protected virtual void Awake()
    {
        if (isPersistent)
        {
            if (instance)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            instance = this as T;
        }
    }
}
