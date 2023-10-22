using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Serialization;

public class PipesSpriteMask : MonoBehaviour, IOnOffObjects
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    [SerializeField] 
    private SpriteMask _spriteMask;
    [SerializeField] 
    private bool startOn;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
        _spriteMask = GetComponent<SpriteMask>();
        if(startOn) TurnOn();
    }

    public void TurnOn()
    {
        _spriteMask.enabled = true;
    }

    public void TurnOff()
    {
        _spriteMask.enabled = false;
    }
}
