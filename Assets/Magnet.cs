using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Magnet : MonoBehaviour, IOnOffObjects
{
    [SerializeField]
    private Animator _lightAnimator;
    [SerializeField]
    //private Animator _bladeAnimator;
    private AreaEffector2D _areaEffector;
    private ParticleSystem _magnetParticles;
    [SerializeField]
    private GameObject _stick;

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
        _lightAnimator.SetBool("Fan_State", true);
       // _bladeAnimator.SetBool("Fan_State", true);
        if (!_magnetParticles.isPlaying) _magnetParticles.Play();
        _areaEffector.enabled = true;
        _stick.SetActive(true);
    }

    public void TurnOff()
    {
        _lightAnimator.SetBool("Fan_State", false);
        //_bladeAnimator.SetBool("Fan_State", false);
        _magnetParticles.Stop();
        _areaEffector.enabled = false;
        _stick.SetActive(false);

    }
}
