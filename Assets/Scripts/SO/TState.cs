using System;
using UnityEngine;



public class TState<T> : ScriptableObject
{
    public event Action<T> Observers;

    [SerializeField] private T _value;

    public  T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            Observers?.Invoke(_value);
        }
    }
}
