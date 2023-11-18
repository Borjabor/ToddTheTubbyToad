using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Fan : MonoBehaviour, IOnOffObjects
{
    [SerializeField]
    private Animator _lightAnimator;
    [SerializeField]
    private Animator _bladeAnimator;
    private AreaEffector2D _areaEffector;
    private ParticleSystem _windParticles;

    [SerializeField] 
    private bool _startOn;
    private bool _isOn;

    private void Awake()
    {
        _areaEffector = GetComponentInChildren<AreaEffector2D>();
        _windParticles = GetComponentInChildren<ParticleSystem>();
        if(_startOn) TurnOn();
    }

    private void Update()
    {
        if (_isOn && !_windParticles.isPlaying)
        {
            _windParticles.Play();
        }
        else if (!_isOn)
        {
            _windParticles.Stop();
        }
    }

    public void TurnOn()
    {
        _lightAnimator.SetBool("Fan_State", true);
        _bladeAnimator.SetBool("Fan_State", true);
        _isOn = true;
        _areaEffector.enabled = true;
    }

    public void TurnOff()
    {
        _lightAnimator.SetBool("Fan_State", false);
        _bladeAnimator.SetBool("Fan_State", false);
        _isOn = false;
        _areaEffector.enabled = false;
    }
}
