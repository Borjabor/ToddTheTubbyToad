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
    private float _currentMaxLength;
    [SerializeField]
    private float _fallSpeed;
    private LineRenderer _lineRenderer;
    [SerializeField]
    private LayerMask _obstableLayerMask;
    [SerializeField]
    private BoxCollider2D _boxCollider2D;
    [SerializeField]
    private GameObject _splashEffect;

    [SerializeField]
    private Material _lavaMaterial;

    
    
    [Header("Geyser Variant")]
    [SerializeField]
    private bool _isGeyser;
    private Vector3 direction;

    [SerializeField]
    private float _activeTime = 3f;
    [SerializeField]
    private float _inactiveTime = 5f;
    private float _elapsedTime;
    private bool _isActive;
    
    private float _lastLength;

    private void Awake()
    {
        direction = _isGeyser ? Vector3.up : -Vector3.up;
        _currentMaxLength = _maxLength;
        _lineRenderer = GetComponent<LineRenderer>();
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
        if(_isGeyser) GeyserTimer();
        
        GetCurrentLength(out float currentMaxLength);
        CalculateActualLength(currentMaxLength, out float currentLength);
        ResizeLine(currentLength);
        RescaleCollider(currentLength);
        CheckForSplash(currentLength, currentMaxLength);
        
    }

    private void GeyserTimer()
    {
        if (!_isActive)
        {
            _currentMaxLength = Mathf.Lerp(_currentMaxLength, 0f, (_inactiveTime / _activeTime) * Time.deltaTime);
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _inactiveTime)
            {
                _isActive = true;
                _elapsedTime = 0f;
            }
        }
        else
        {
            _currentMaxLength = _maxLength;
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _activeTime)
            {
                _elapsedTime = 0f;
                _isActive = false;
            }
        }
    }

    private void GetCurrentLength(out float currentMaxLength)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _currentMaxLength, _obstableLayerMask);
        currentMaxLength = hit ? Vector2.Distance(transform.position, hit.point) : _currentMaxLength;
    }

    private void CalculateActualLength(float currentMaxLength, out float currentLength)
    {
        currentLength = _lastLength + Time.deltaTime * _fallSpeed;
        currentLength = Mathf.Clamp(currentLength, 0, currentMaxLength);
        _lastLength = currentLength;
    }

    private void ResizeLine(float currentLength)
    {
        _lineRenderer.SetPosition(1, transform.position + direction * currentLength);
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
        _splashEffect.transform.position = transform.position + direction * currentLength;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _boxCollider2D.size.x/2);
    }
}
