using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class FollowPath : ObjectFSM, IOnOffObjects
{
    [SerializeField]
    private GameState _gameState;
    
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

    [SerializeField]
    private GameObject _spriteShapeOff = null;
    [SerializeField]
    private GameObject _platformOff = null;
    [SerializeField]
    private GameObject _spriteShapeOn = null;
    [SerializeField]
    private GameObject _platformOn = null;

    void Awake()
    {
        _gameState = Resources.Load<GameState>("SOAssets/Game State");
        _nodes = GetComponentsInChildren<Node>();
        if(_nodes.Length == 2) _loop = true;
        _currentNodePosition = _nodes[_currentNodeIndex].transform.position;
        if (_startOn) TurnOn();
        if (_loop) _timeToWait = 0f;

        //_spriteShapeOn.SetActive(false);
        //_platformOn.SetActive(false);


    }

    public void TurnOn()
    {
        SetState(On());
        _spriteShapeOn.SetActive(true);
        _platformOn.SetActive(true);
        _spriteShapeOff.SetActive(false);
        _platformOff.SetActive(false);



    }

    public void TurnOff()
    {
        SetState(Off());
        _spriteShapeOn.SetActive(false);
        _platformOn.SetActive(false);
        _spriteShapeOff.SetActive(true);
        _platformOff.SetActive(true);
    }

    protected override IEnumerator On()
    {
        while ((Vector2)_movingObject.transform.position != _currentNodePosition)
        {
            yield return new WaitUntil(() => _gameState.Value == States.NORMAL);
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
        if (_currentNodeIndex > _nodes.Length - 1) _currentNodeIndex = _nodes.Length - 1;
        _currentNodePosition = _nodes[_currentNodeIndex].transform.position;
        SetState(On());
    }

    protected override IEnumerator Off()
    {
        yield break;
    }
}
