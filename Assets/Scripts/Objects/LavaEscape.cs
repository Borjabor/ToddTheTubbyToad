using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEscape : FollowPath
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _minSpeed, _maxSpeed, _speedRatio = 2;

    protected override void Awake()
    {
        _gameState = Resources.Load<GameState>("SOAssets/Game State");
        _nodes = GetComponentsInChildren<Node>();
        if(_nodes.Length == 2) _loop = true;
        _currentNodePosition = _nodes[_currentNodeIndex].transform.position;
        if (_startOn) TurnOn();
        if (_loop) _timeToWait = 0f;
    }

    private void Update()
    {
        _moveSpeed = Mathf.Clamp((_target.transform.position.y - _movingObject.transform.position.y)/_speedRatio, _minSpeed, _maxSpeed);
        _moveSpeed = Mathf.Clamp((_target.transform.position.y - _movingObject.transform.position.y)/_speedRatio, _minSpeed, _maxSpeed);
    }
}
