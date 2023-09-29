using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
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
        if(!_audioSource.isPlaying) _audioSource.Play();
        
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            //Destroy(gameObject);
            Debug.Log($"hit check");
            StartCoroutine(Dissolve());
        }
        
        var sticky = other.gameObject.GetComponent<StickySurface>();
        if (sticky) _rb.bodyType = RigidbodyType2D.Static;
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
