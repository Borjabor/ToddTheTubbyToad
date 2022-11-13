using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [HideInInspector]
    public static bool _isHolding = false;
    
    [Header("Audio")]
    [SerializeField] 
    private AudioClip _grabbing;
    private AudioSource _playerAudio;

    private void Awake()
    {
        _playerAudio = GetComponent<AudioSource>();

    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _isHolding = true;
        }
        else
        {
            _isHolding = false;
            Tentacle.GrabbedObject = null;
            Destroy(GetComponent<FixedJoint2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isHolding && other.gameObject.CompareTag("Object"))
        {
            Rigidbody2D rb = other.transform.GetComponent<Rigidbody2D>();
            Tentacle.GrabbedObject = other.gameObject;
            if (rb != null)
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                fj.connectedBody = rb;
                _playerAudio.PlayOneShot(_grabbing);
            }
            else
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            }
        }
    }
}
