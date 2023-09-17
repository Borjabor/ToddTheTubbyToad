using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ConveyorBelt : MonoBehaviour, IOnOffObjects
{
    private Animator _beltAnimator;
    private SurfaceEffector2D _surfaceEffector;

    [SerializeField] 
    private bool startOn;
    private void Awake()
    {
        _beltAnimator = GetComponent<Animator>();
        _surfaceEffector = GetComponent<SurfaceEffector2D>();
        if(startOn) TurnOn();
    }

    public void TurnOn()
    {
        _beltAnimator.SetBool("PressButton", true);
        _surfaceEffector.enabled = true;
    }

    public void TurnOff()
    {
        _beltAnimator.SetBool("PressButton", false);
        _surfaceEffector.enabled = false;
    }
}