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
        var duration = _audioSource.clip.length * 0.6f;
        var timeElapsed = 0f;
        var startPos = transform.position;
        var targetPos = _targetPos.transform.position;
        
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
        else
        {
            _audioSource.Play();
        }
        yield return new WaitForSeconds(0.7f);
        while (timeElapsed < duration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, timeElapsed/duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        // while (transform.position != _targetPos.transform.position)
        // {
        //     if(!_audioSource.isPlaying && transform.position.y != _targetPos.transform.position.y) _audioSource.Play();
        //     //transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, _openSpeed * Time.deltaTime);
        //     Debug.Log($"Open");
        //     yield return null;
        // }
    }

    protected override IEnumerator Off()
    {
        var duration = _audioSource.clip.length * 0.6f;
        var timeElapsed = 0f;
        var startPos = transform.position;
        var targetPos = _startPos;

        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
        else
        {
            _audioSource.Play();
        }
        yield return new WaitForSeconds(0.7f);
        while (timeElapsed < duration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, timeElapsed/duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        // while ((Vector2)transform.position != _startPos)
        // {
        //     if(!_audioSource.isPlaying && transform.position.y != _startPos.y) _audioSource.Play();
        //     transform.position = Vector2.MoveTowards(transform.position, _startPos, _openSpeed * Time.deltaTime);
        //     Debug.Log($"Close");
        //     yield return null;
        // }
    }

}
