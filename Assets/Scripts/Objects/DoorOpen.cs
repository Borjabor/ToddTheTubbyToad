using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour, IOnOffObjects 
{
    [SerializeField] private GameObject _targetPos;
    private Vector2 _startPos;
    [SerializeField] private float _openSpeed = 4f;
    private bool _isOpening;
    private AudioSource _audioSource;

    private void Awake()
    {
        _startPos = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (_isOpening)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, _openSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _targetPos.transform.position.y) _audioSource.Play();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _openSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _startPos.y) _audioSource.Play();
        }
    }

    public void TurnOn()
    {
        _isOpening = true;
        //transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, _openSpeed * Time.deltaTime);
    }
    
    public void TurnOff()
    {
        _isOpening = false;
        //transform.position = Vector2.MoveTowards(transform.position, _startPos, _openSpeed * Time.deltaTime);
    }

}
