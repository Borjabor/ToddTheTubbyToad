using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _swingForce = 20f;
    
    void Awake()
    {
        _rigidBody.AddForce (transform.right * _swingForce);
    }

}
