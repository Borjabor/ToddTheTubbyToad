using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEscape : FollowPath
{
    [SerializeField]
    private GameObject _target;
    private float _cameraShakeAmount;
    [SerializeField]
    private float _maxCameraShakeAmount = 10f;

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
        if (!_isOn) return;
        _cameraShakeAmount = Mathf.Clamp(_maxCameraShakeAmount/_moveSpeed, 0f, _maxCameraShakeAmount);
        CameraManager.Instance.ShakeCamera(_cameraShakeAmount, 1f);
    }
}
