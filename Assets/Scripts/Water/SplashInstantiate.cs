using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject _waterSplash;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(_waterSplash, other.transform.position, Quaternion.identity);
    }
}
