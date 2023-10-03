using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Serialization;

public class Waterfall : MonoBehaviour
{
    [SerializeField]
    private float _maxLength;
    [SerializeField]
    private float _fallSpeed;
    private LineRenderer _lineRenderer;
    [SerializeField]
    private LayerMask _obstableLayerMask;
    [SerializeField]
    private BoxCollider2D _boxCollider2D;
    [SerializeField]
    private GameObject _splashEffect;

    
    
    private float _lastLength;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        //_boxCollider2D = GetComponent<BoxCollider2D>();
        _lineRenderer.startWidth = _boxCollider2D.size.x;
        _lineRenderer.endWidth = _boxCollider2D.size.x;
        _lineRenderer.positionCount = 2;
        for (int i = 0; i < 2; i++)
        {
            _lineRenderer.SetPosition(i, transform.position);
        }
    }

    private void Update()
    {
        GetCurrentLength(out float currentMaxLength);
        CalculateActualLength(currentMaxLength, out float currentLength);
        ResizeLine(currentLength);
        RescaleCollider(currentLength);
        CheckForSplash(currentLength, currentMaxLength);
    }

    private void GetCurrentLength(out float currentMaxLength)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _maxLength, _obstableLayerMask);
        currentMaxLength = hit ? Vector2.Distance(transform.position, hit.point) : _maxLength;
    }

    private void CalculateActualLength(float currentMaxLength, out float currentLength)
    {
        currentLength = _lastLength + Time.deltaTime * _fallSpeed;
        currentLength = Mathf.Clamp(currentLength, 0, currentMaxLength);
        _lastLength = currentLength;
    }

    private void ResizeLine(float currentLength)
    {
        _lineRenderer.SetPosition(1, transform.position - Vector3.up * currentLength);
        _lineRenderer.material.SetFloat("_Length", currentLength);
    }

    private void RescaleCollider(float currentLength)
    {
        _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, currentLength);
        _boxCollider2D.offset = new Vector2(0, -currentLength / 2);
    }

    private void CheckForSplash(float currentLength, float currentMaxLength)
    {
        _splashEffect.SetActive(currentLength >= currentMaxLength);
        _splashEffect.transform.position = transform.position - Vector3.up * currentLength;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _boxCollider2D.size.x/2);
    }
}
