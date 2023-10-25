using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonScript : ObjectFSM
{
    private bool _isPressed;
    private Vector3 _oringialPos;
    [Tooltip("Drag button here")]
    [SerializeField] 
    private GameObject _targetPos;
    [Tooltip("Drag Desired Affected Object Here")]
    [SerializeField]
    private GameObject _affectedObject;
    private IOnOffObjects _affectedObjectI;
    private BoxCollider2D _collider;

    [SerializeField]
    private GameObject _pipeMask = null;

    [SerializeField]
    private Animator button;
    private AudioSource _audioSource;
    private bool _hasPlayed;

    private void Awake()
    {
        _pipeMask.SetActive(false);
        _oringialPos = transform.position;
        _audioSource = GetComponent<AudioSource>();
        _affectedObjectI = _affectedObject.GetComponent<IOnOffObjects>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Object")
        {
            if(!_isPressed) SetState(On());
            _isPressed = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Object")
        {
            if(!_isPressed) SetState(On());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapAreaAll(_collider.bounds.min,_collider.bounds.max);
        int rigidBodies = 0;
        foreach (Collider2D collider2D in collider2Ds)
        {
            if (collider2D.GetComponent<Rigidbody2D>() && !collider2D.GetComponent<Tilemap>()) rigidBodies++;
        }
        
        if (rigidBodies < 1) SetState(Off());
    }

    protected override IEnumerator On()
    {
        while (transform.position != _targetPos.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, Time.deltaTime);
            button.SetBool("Button_State", true);
            if (!_hasPlayed && transform.position.y == _targetPos.transform.position.y)
            {
                _audioSource.Play();
                _hasPlayed = true;
            }
            _hasPlayed = false;
            if (Vector2.Distance(transform.position, _targetPos.transform.position) <= 0.1f) break;
            yield return 0;
        }
        _pipeMask.SetActive(true);
        _affectedObjectI.TurnOn();
    }

    protected override IEnumerator Off()
    {
        _pipeMask.SetActive(false);
        _affectedObjectI.TurnOff();
        _isPressed = false;
        while (transform.position.y < _oringialPos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, _oringialPos, Time.deltaTime);
            button.SetBool("Button_State", false);
            if (Vector2.Distance(transform.position, _oringialPos) <= 0.1f) break;
            yield return 0;
        }
    }
}