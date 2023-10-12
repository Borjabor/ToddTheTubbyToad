using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnterCollider : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioTriggerEnter;

    [SerializeField]
    private AudioSource _audioCollisionEnter;

    private void Awake()
    {
        _audioCollisionEnter = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!_audioCollisionEnter.isPlaying) _audioCollisionEnter.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_audioTriggerEnter.isPlaying) _audioTriggerEnter.Play();
    }

}
