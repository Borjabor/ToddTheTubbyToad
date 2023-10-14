using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private PlayerState _playerState;
    private GameState _gameState;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    [SerializeField]
    private CharacterController _player;
    private float _moveSpeed = 60f;
    private float _horizontalMove;

    private void Awake()
    {
        _gameState = Resources.Load<GameState>("SOAssets/Game State");
        _playerState = Resources.Load<PlayerState>("SOAssets/Player State");
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _moveSpeed = _player._moveSpeed;
    }
    
    void Update()
    {
        if (_gameState.Value != States.NORMAL) return;
        if(_playerState.Value == PlayerStates.NORMAL) return;
        
        GetInputs();
    }

    private void GetInputs()
    {
        _horizontalMove = Input.GetAxis("Horizontal") * _moveSpeed;
    }

    private void FixedUpdate()
    {
        if (_gameState.Value != States.NORMAL) return;
        if(_playerState.Value == PlayerStates.NORMAL) return;
        Move(_horizontalMove * Time.fixedDeltaTime);
    }


    public void Pop()
    {
        Destroy(gameObject);
    }

    public void Move(float move)
    {
        _rb.AddForce(new Vector2(move * 10f, 0f));
    }

    public void Collide()
    {
        _collider.isTrigger = false;
    }
}
