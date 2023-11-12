using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ConveyorBelt : MonoBehaviour, IOnOffObjects
{
    private Animator _beltAnimator;
    private SurfaceEffector2D _surfaceEffector;
    private AudioSource _audioSource;
    [SerializeField] 
    private bool startOn;

    private void Awake()
    {
        _beltAnimator = GetComponent<Animator>();
        _surfaceEffector = GetComponent<SurfaceEffector2D>();
        _audioSource = GetComponent<AudioSource>();
        if(startOn) TurnOn();
    }

    public void TurnOn()
    {
        _beltAnimator.SetBool("PressButton", true);
        _surfaceEffector.enabled = true;
        if(!_audioSource.isPlaying) _audioSource.Play();
    }

    public void TurnOff()
    {
        _beltAnimator.SetBool("PressButton", false);
        _surfaceEffector.enabled = false;
        _audioSource.Stop();
    }
}