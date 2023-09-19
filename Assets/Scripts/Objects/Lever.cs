using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Lever : MonoBehaviour
{
    private HingeJoint2D _hj;
    [Tooltip("Drag Desired Affected Object Here")]
    [SerializeReference]
    private GameObject _affectedObject;
    private IOnOffObjects _affectedObjectI;


    [Header("Lever:")]
    public Animator lever;
    private AudioSource _audioSource;
    [SerializeField] ParticleSystem _eletricity;

    private bool _hasPlayed = false;

    private void Awake()
    {
        _hj = GetComponent<HingeJoint2D>();
        _audioSource = GetComponent<AudioSource>();
        _affectedObjectI = _affectedObject.GetComponent<IOnOffObjects>();
    }

    private void Update()
    {
        if (_hj.jointAngle <= -85)
        {
            _affectedObjectI.TurnOn();
            lever.SetBool("Lever_State", true);
            if (!_hasPlayed)
            {
                _eletricity.Play();
                _audioSource.Play();
                _hasPlayed = true;
            }
        }
        else if (_hj.jointAngle >= -55)
        {
            _affectedObjectI.TurnOff();
            lever.SetBool("Lever_State", false);
            if (_hasPlayed)
            {
                _audioSource.Play();
                _hasPlayed = false;
            }
        }
    }
    
    public void Particles()
    {
        if (!_eletricity.isPlaying)
        {
            _eletricity.Play();
        }

    }
}