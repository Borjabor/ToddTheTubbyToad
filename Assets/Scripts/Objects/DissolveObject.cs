using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveObject : MonoBehaviour
{
    private AudioSource _audioSource;
    private Renderer _renderer;
    private Rigidbody2D _rb;
    private float _dissolveAmount = 1f;
    private bool _isDissolving;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb.velocity.magnitude <= 2f) return;
            if (!_audioSource.isPlaying) _audioSource.Play();
        }
        else
        {
            if (!_audioSource.isPlaying && _rb.velocity.magnitude >= 2.5f) _audioSource.Play();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dissolve"))
        {
            StartCoroutine(Dissolve());
        }
    }
    private IEnumerator Dissolve()
    {
        while (_dissolveAmount > 0)
        {
            _renderer.material.SetFloat("_DissolveAmount", _dissolveAmount);
            yield return null;
            _dissolveAmount -= Time.deltaTime;
        }

        if (_dissolveAmount <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
