using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectFSM : MonoBehaviour
{
    protected Coroutine _currentState;
    
    protected void SetState(IEnumerator newState)
    {
        if (_currentState != null)
        {
            StopCoroutine(_currentState);
        }
        _currentState = StartCoroutine(newState);
    }

    protected abstract IEnumerator On();

    protected abstract IEnumerator Off();
}
