using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject _waterSplashSmall;
    [SerializeField]
    private GameObject _waterSplashLarge;
    [SerializeField]
    private float _splashSizeThreshold = 10;

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.relativeVelocity.magnitude > _splashSizeThreshold)
        {
            Instantiate(_waterSplashLarge, other.transform.position, Quaternion.identity);
        }

        else
        {
            Instantiate(_waterSplashSmall, other.transform.position, Quaternion.identity);
        }

    }
}
