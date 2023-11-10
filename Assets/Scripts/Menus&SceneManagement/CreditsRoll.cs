using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsRoll : MonoBehaviour
{
    [SerializeField]
    private float _scrollSpeed = 1f;
    private RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var transformPosition = _transform.position;
        transformPosition.y += _scrollSpeed * Time.deltaTime;
        _transform.position = transformPosition;
    }
}
