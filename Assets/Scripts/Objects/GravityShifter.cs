using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShifter : MonoBehaviour, IOnOffObjects
{
    [SerializeField]
    private bool _isOn;
    private BoxCollider2D _collider;
    [SerializeField]
    private LayerMask _shiftLayerMask;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void TurnOn()
    {
        _isOn = true;
        Collider2D[] collider2Ds = Physics2D.OverlapAreaAll(_collider.bounds.min,_collider.bounds.max, _shiftLayerMask);
        foreach (Collider2D collider2D in collider2Ds)
        {
            var rb = collider2D.GetComponent<Rigidbody2D>();
            if (rb) rb.gravityScale *= -1.0f;
        }
    }

    public void TurnOff()
    {
        _isOn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isOn) return;
        var rb = other.GetComponent<Rigidbody2D>();
        if (rb) rb.gravityScale *= -1.0f;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_isOn) return;
        var rb = other.GetComponent<Rigidbody2D>();
        if (rb) rb.gravityScale *= -1.0f;
    }
}
