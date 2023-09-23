using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShifter : MonoBehaviour, IOnOffObjects
{
    [SerializeField]
    private bool _isOn;
    private BoxCollider2D _collider;
    [SerializeField] private LayerMask _shiftLayerMask;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void TurnOn()
    {
        Debug.Log($"On");
        Collider2D[] collider2Ds = Physics2D.OverlapAreaAll(_collider.bounds.min,_collider.bounds.max);
        foreach (Collider2D collider2D in collider2Ds)
        {
            Debug.Log($"Called");
            if (collider2D.GetComponent<Rigidbody2D>() != null)
            {
                collider2D.GetComponent<Rigidbody2D>().gravityScale *= -1.0f;
            }
            Debug.Log($"{collider2D.name}");
        }
    }
    
    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(transform.position, 5.0f);
        // Gizmos.DrawCube(_collider.bounds.center, new Vector3(5f,5f, 5f));
    }

    public void TurnOff()
    {
        _isOn = false;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (!_isOn) return;
    //     Debug.Log($"Detect");
    //     if (other.GetComponent<Rigidbody2D>())
    //     {
    //         other.GetComponent<Rigidbody2D>().gravityScale *= -1f;
    //     }
    // }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_isOn) return;
        if (other.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().gravityScale *= -1f;
        }
    }
}
