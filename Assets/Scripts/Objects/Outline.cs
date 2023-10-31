using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _spriteRenderer.enabled = true;
        Debug.Log("Entered Collider");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _spriteRenderer.enabled = false;
        Debug.Log("Exited Collider");
    }

}
