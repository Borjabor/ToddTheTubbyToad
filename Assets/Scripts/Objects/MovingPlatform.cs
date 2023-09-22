using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : ObjectFSM, IOnOffObjects
{
    [SerializeField] private GameObject _targetPos;
    private Vector2 _startPos;
    [SerializeField] private float _moveSpeed = 4f;
    private AudioSource _audioSource;
    
    public void TurnOn()
    {
        throw new System.NotImplementedException();
    }

    public void TurnOff()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator On()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Off()
    {
        throw new System.NotImplementedException();
    }
}
