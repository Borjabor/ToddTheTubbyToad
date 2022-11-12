using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] 
    private GameObject _closeSprite;
    [SerializeField] 
    private GameObject _openSprite;

    [SerializeField] private ParticleSystem _particles;
    

    private Collider2D _checkpointCollider;

    private void Awake()
    {
        //_closeSprite.SetActive(true);
        //_openSprite.SetActive(false);
        _checkpointCollider = GetComponent<BoxCollider2D>();
        //_checkpointCollider.isTrigger = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            Destroy(other.gameObject);
        }
    }
}
