using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private Bubble _currentBubble;

    [SerializeField]
    private bool _startInBubble;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if(_startInBubble) _rb.isKinematic = true;
    }

    private void Update()
    {
        if(!_currentBubble) _rb.isKinematic = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var sticky = other.gameObject.GetComponent<StickySurface>();
        if (sticky) _rb.bodyType = RigidbodyType2D.Static;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Bubble>()) return;
        _currentBubble = other.gameObject.GetComponent<Bubble>();
        StartCoroutine(GetInBubble());
    }
    
    private IEnumerator GetInBubble()
    {
        transform.SetParent(_currentBubble.transform);
        _rb.isKinematic = true;
        while (transform.position != _currentBubble.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentBubble.transform.position, 0.01f);
            yield return null;
        }

        if (Vector2.Distance(transform.position, _currentBubble.transform.position) <= 0.1f)
            transform.position = _currentBubble.transform.position;
    }
}
