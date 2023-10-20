using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Serialization;

public class PipesSpriteMask : MonoBehaviour
{
    private GameObject _spriteMask;

    [SerializeField] 
    private bool _isOn = false;
    [SerializeField] 
    private bool startOn;

    private void Awake()
    {
        _spriteMask = GetComponent<PipesSpriteMask>();
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
