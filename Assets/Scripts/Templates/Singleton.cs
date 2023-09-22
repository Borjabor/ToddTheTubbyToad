using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance;

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<T>();
            SingletonAwake();
        }
        else {
            Destroy(this.gameObject);
        }
    }

    abstract protected void SingletonAwake();
}
