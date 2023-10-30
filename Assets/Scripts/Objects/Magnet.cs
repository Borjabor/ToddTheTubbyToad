using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Magnet : MonoBehaviour, IOnOffObjects
{
    [SerializeField]
    private Animator _lightAnimator;
    private AreaEffector2D _areaEffector;
    private ParticleSystem _magnetParticles;
    
    [SerializeField]
    private bool _startOn;

    private void Awake()
    {
        _areaEffector = GetComponentInChildren<AreaEffector2D>();
        _magnetParticles = GetComponentInChildren<ParticleSystem>();
        if (_startOn) TurnOn();
    }

    public void TurnOn()
    {
        _lightAnimator.SetBool("Magnet_State", true);
       // _bladeAnimator.SetBool("Fan_State", true);
        _magnetParticles.Play();
        _areaEffector.enabled = true;
        //_parent.SetActive(true);
    }

    public void TurnOff()
    {
        _lightAnimator.SetBool("Magnet_State", false);
        //_bladeAnimator.SetBool("Fan_State", false);
        _magnetParticles.Stop();
        _areaEffector.enabled = false;
        //_parent.SetActive(false);

    }

    }

