using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDestroy: MonoBehaviour
{
    private float _destroyTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,_destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
