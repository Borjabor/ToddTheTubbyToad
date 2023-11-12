using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorSounds : MonoBehaviour
{
    private Effector2D _effector;
    private AudioSource _audioSource;

    

    private void Awake()
    {
        _effector = GetComponent<Effector2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_effector.enabled && !_audioSource.isPlaying) _audioSource.Play();
    }
}
