using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private Material _material;
    [SerializeField] private Renderer _renderer;
    private float _dissolveAmount = 1f;
    private bool _isDissolving;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
    }

    private IEnumerator Dissolve()
    {
        while (_dissolveAmount > 0)
        {
            //_material.SetFloat("_DissolveAmount", _dissolveAmount);
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
