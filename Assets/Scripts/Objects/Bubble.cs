using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void Pop()
    {
        Destroy(gameObject);
    }

    public void Move(float move)
    {
        _rb.AddForce(new Vector2(move * 5f, 0f));
    }

    public void Collide()
    {
        _collider.isTrigger = false;
    }
}
