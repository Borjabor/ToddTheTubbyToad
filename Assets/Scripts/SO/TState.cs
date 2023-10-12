using System;
using UnityEngine;



public class TState<T> : ScriptableObject
{
    public event Action<T> StateChange;

    [SerializeField] private T _value;

    public  T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            StateChange?.Invoke(_value);
        }
    }
}
