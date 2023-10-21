using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TAsset<T> : ScriptableObject
{

    public static implicit operator T(TAsset<T> v) => v.Value;

    public event Action<T> Observers;
    [SerializeField]
    protected T _value;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            NotifyObservers();
        }
    }

    public void NotifyObservers()
    {
        Observers?.Invoke(_value);
    }
}
