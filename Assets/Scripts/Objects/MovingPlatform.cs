using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : ObjectFSM, IOnOffObjects
{
    [SerializeField] 
    private GameObject _targetPos;
    private Vector2 _startPos;
    [SerializeField] 
    private float _moveSpeed = 4f;
    private AudioSource _audioSource;
    [SerializeField]
    private bool _startOn;

    private void Awake()
    {
        if (_startOn) TurnOn();
        _startPos = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_startOn)
        {
            if(_currentState == null) TurnOn();
        }
        else
        {
            TurnOff();
        }
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
        while (transform.position != _targetPos.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, _moveSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _targetPos.transform.position.y) _audioSource.Play();
            if (Vector2.Distance(transform.position, _targetPos.transform.position) <= 0.1f)
            {
                yield return new WaitForSeconds(2f);
                SetState(GoBack());
            }
            yield return 0;
        }
    }

    private IEnumerator GoBack()
    {
        while ((Vector2)transform.position != _startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _moveSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _startPos.y) _audioSource.Play();
            if(Vector2.Distance(transform.position, _startPos) <= 0.1f)
            {
                yield return new WaitForSeconds(2f);
                SetState(On());
            }
            yield return 0;
        }
    }

    protected override IEnumerator Off()
    {
        yield break;
    }
}
