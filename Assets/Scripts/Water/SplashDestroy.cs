using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDestroy: MonoBehaviour
{
    private float _destroyTime = 2;

    void Start()
    {
        Destroy(gameObject,_destroyTime);
    }
}
