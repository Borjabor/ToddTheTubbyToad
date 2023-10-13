using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : ObjectFSM, IOnOffObjects
{
    private Node[] _nodes;

    [SerializeField]
    private GameObject _movingObject;
    [SerializeField]
    private float _moveSpeed;
    private int _currentNodeIndex;
    private Vector2 _currentNodePosition;
    private bool _forward;
    private AudioSource _audioSource;

    [SerializeField]
    private bool _startOn;
    [SerializeField]
    private bool _loop;
    [SerializeField]
    private float _timeToWait = 2f;
    
    void Awake()
    {
        _nodes = GetComponentsInChildren<Node>();
        _currentNodePosition = _nodes[_currentNodeIndex].transform.position;
        if (_startOn) TurnOn();
        if (_loop) _timeToWait = 0f;
    }

    public void TurnOn()
    {
        SetState(On());
    }

    public void TurnOff()
    {
        SetState(Off());
    }

    protected override IEnumerator On()
    {
        while ((Vector2)_movingObject.transform.position != _currentNodePosition)
        {
            _movingObject.transform.position = Vector2.MoveTowards(_movingObject.transform.position, _currentNodePosition, _moveSpeed * Time.deltaTime);
            yield return 0;
        }
        SetState(GetNextNode());
    }

    private IEnumerator GetNextNode()
    {
        if (_currentNodeIndex == 0)
        {
            _forward = true;
            yield return new WaitForSeconds(_timeToWait);
        }
        else if (_currentNodeIndex >= _nodes.Length - 1)
        {
            if (_loop)
            {
                _currentNodeIndex = -1;
            }
            else
            {
                _forward = false;
            }
            yield return new WaitForSeconds(_timeToWait);
        }

        _currentNodeIndex = _forward ? _currentNodeIndex + 1 : _currentNodeIndex - 1;
        _currentNodePosition = _nodes[_currentNodeIndex].transform.position;
        SetState(On());
    }

    protected override IEnumerator Off()
    {
        yield break;
    }
}
