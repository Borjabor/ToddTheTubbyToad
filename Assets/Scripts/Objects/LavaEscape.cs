using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEscape : FollowPath
{
    private Camera _mainCamera;

    [SerializeField]
    private float _minSpeed, _maxSpeed;

    protected override void Awake()
    {
        _gameState = Resources.Load<GameState>("SOAssets/Game State");
        _mainCamera = Camera.main;
        _nodes = GetComponentsInChildren<Node>();
        if(_nodes.Length == 2) _loop = true;
        _currentNodePosition = _nodes[_currentNodeIndex].transform.position;
        if (_startOn) TurnOn();
        if (_loop) _timeToWait = 0f;
    }

    private void Update()
    {
        _moveSpeed = Mathf.Clamp(Vector2.Distance(_mainCamera.transform.position, transform.position)/2f, _minSpeed, _maxSpeed);
        Debug.Log($"{_moveSpeed}");
    }
}
