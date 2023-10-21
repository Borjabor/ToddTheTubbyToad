using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Serialization;

public class PipesSpriteMask : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    [SerializeField] 
    private SpriteMask _spriteMask;
    [SerializeField] 
    private bool _isOn = false;
    [SerializeField] 
    private bool startOn;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
        _spriteMask = GetComponent<SpriteMask>();
        TurnOff();
        if(startOn) TurnOn();
    }

    public void TurnOn()
    {
        _spriteRenderer.enabled = true;
        _spriteMask.enabled = true;
        _isOn = true;
    }

    public void TurnOff()
    {
        _spriteRenderer.enabled = false;
        _spriteMask.enabled = false;
        _isOn = false;
    }
}
