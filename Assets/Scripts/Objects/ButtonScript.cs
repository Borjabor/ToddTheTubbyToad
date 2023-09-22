using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private Vector3 _oringialPos;
    [Tooltip("Drag button here")]
    [SerializeField] private GameObject _targetPos;
    bool moveBack = false;
    [Tooltip("Drag Desired Affected Object Here")]
    [SerializeReference]
    private GameObject _affectedObject;
    private IOnOffObjects _affectedObjectI;

    public Animator button;
    private AudioSource _audioSource;
    private bool _hasPlayed = false;

    private void Awake()
    {
        _oringialPos = transform.position;
        _audioSource = GetComponent<AudioSource>();
        _affectedObjectI = _affectedObject.GetComponent<IOnOffObjects>();
    }
    
    private void Update()
    {
        if (moveBack)
        {
            if (transform.position.y < _oringialPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                moveBack = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Object")
        {
            StartCoroutine(Push());
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Object")
        {
            collision.transform.parent = transform;
        }
        button.SetBool("Button_State", false);
        moveBack = true;
        collision.transform.parent = null;
        _hasPlayed = false;
        _affectedObjectI.TurnOff();
    }

    private IEnumerator Push()
    {
        while (transform.position != _targetPos.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, Time.deltaTime);
            button.SetBool("Button_State", true);
            moveBack = false;
            if (!_hasPlayed && transform.position.y == _targetPos.transform.position.y)
            {
                _audioSource.Play();
                _hasPlayed = true;
            }

            if (Vector2.Distance(transform.position, _targetPos.transform.position) <= 0.1f) break;
            yield return 0;
        }
        _affectedObjectI.TurnOn();
    }
}
