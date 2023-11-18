using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Gross = Animator.StringToHash("Gross");
    private static readonly int Hook = Animator.StringToHash("Hook");
    private static readonly int OutOfRange = Animator.StringToHash("OutOfRange");
    private int _currentState;
    private float _lockedTill;
    private bool _gross;
    private bool _hook;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var state = GetState();
        
        if(state == _currentState) return;
        _animator.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        if (_gross) return LockState(Gross, 0.2f);
        return _hook ? Hook : Idle;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    public void SetState(string state)
    {
        switch (state)
        {
            case "Gross":
                _gross = true;
                _hook = false;
                break;
            case "Hook":
                _gross = false;
                _hook = true;
                break;
            case "Idle":
                _gross = false;
                _hook = false;
                break;
        }
    }
}
