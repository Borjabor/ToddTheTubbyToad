using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithCheckpoint : MonoBehaviour
{
    private PlayerState _playerState;
    private Vector2 _checkpoint;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _playerState = Resources.Load<PlayerState>("SOAssets/Player State");
        _rb = GetComponent<Rigidbody2D>();
        _checkpoint = transform.position;
    }

    private void OnEnable()
    {
        _playerState.StateChange += Respawn;
    }

    private void OnDisable()
    {
        _playerState.StateChange -= Respawn;
    }

    private void Respawn(PlayerStates obj)
    {
        if(obj == PlayerStates.RESPAWN) StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        _rb.velocity = Vector2.zero;
        transform.position = _checkpoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ObjectCheckpoint"))
        {
            _checkpoint = other.transform.position;
        }
    }
}
