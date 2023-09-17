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

    private void Awake()
    {
        _areaEffector = GetComponentInChildren<AreaEffector2D>();
        _windParticles = GetComponentInChildren<ParticleSystem>();
        if(_startOn) TurnOn();
    }

    public void TurnOn()
    {
        _lightAnimator.SetBool("Fan_State", true);
        _bladeAnimator.SetBool("Fan_State", true);
        if (!_windParticles.isPlaying) _windParticles.Play();
        _areaEffector.enabled = true;
    }

    public void TurnOff()
    {
        _lightAnimator.SetBool("Fan_State", false);
        _bladeAnimator.SetBool("Fan_State", false);
        _windParticles.Stop();
        _areaEffector.enabled = false;
    }
}
