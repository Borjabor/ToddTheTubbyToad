using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : ObjectFSM, IOnOffObjects 
{
    [SerializeField] private GameObject _targetPos;
    private Vector2 _startPos;
    [SerializeField] private float _openSpeed = 4f;
    private AudioSource _audioSource;

    public Animator DoorAnimator;

    private void Awake()
    {
        _startPos = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }

    public void TurnOn()
    {
        SetState(On());
        DoorAnimator.SetBool("Is_Open", true);
    }
    
    public void TurnOff()
    {
        SetState(Off());
        DoorAnimator.SetBool("Is_Open", false);
    }

    protected override IEnumerator On()
    {
        while (transform.position != _targetPos.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, _openSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _targetPos.transform.position.y) _audioSource.Play();
            Debug.Log($"Open");
            yield return 0;
        }
    }

    protected override IEnumerator Off()
    {
        while ((Vector2)transform.position != _startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _openSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _startPos.y) _audioSource.Play();
            Debug.Log($"Close");
            yield return 0;
        }
    }

}
