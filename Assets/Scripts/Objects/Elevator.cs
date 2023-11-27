using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : FollowPath
{
    private Rigidbody2D _rb;
    protected override void Awake()
    {
        base.Awake();
        _rb = _movingObject.GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (_gameState.Value != States.NORMAL) return;
        if(!_isOn) return;
        if (Vector2.Distance(_movingObject.transform.position, _currentNodePosition) <= 0.1f) return;
        var dir = _currentNodePosition - _rb.position;
        _rb.MovePosition(_rb.position + dir.normalized * _moveSpeed * Time.fixedDeltaTime);
    }
    
    protected override IEnumerator On()
    {
        _isOn = true;
        while (Vector2.Distance(_movingObject.transform.position, _currentNodePosition) >= 0.1f)
        {
            Debug.Log($"Far");
            yield return null;
        }

        if (Vector2.Distance(_movingObject.transform.position, _currentNodePosition) <= 0.1f)
        {
            SetState(GetNextNode());
            Debug.Log($"Close");
        }
    }
}
