using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplashInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject _waterSplashSmall;
    [SerializeField]
    private GameObject _waterSplashLarge;
    [SerializeField]
    private float _splashSizeThreshold = 10;

    [SerializeField]
    private float _cooldown = 1;
    private float _lastSplash;

    [SerializeField]
    private AudioSource _bigSplash;
    [SerializeField]
    private AudioSource _smallSplash;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Time.time - _lastSplash < _cooldown)
        {
            return;
        }

        _lastSplash = Time.time;

        if (other.relativeVelocity.magnitude > _splashSizeThreshold)
        {
            Instantiate(_waterSplashLarge, other.transform.position, Quaternion.identity);

            _bigSplash.pitch = (Random.Range(0.7f, 1.2f));
            _bigSplash.Play();

        }

        else
        {
            Instantiate(_waterSplashSmall, other.transform.position, Quaternion.identity);

            _smallSplash.pitch = (Random.Range(0.6f, 1.5f));
            _smallSplash.Play();
        }
    }
}
