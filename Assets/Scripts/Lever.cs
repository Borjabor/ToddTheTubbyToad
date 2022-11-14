using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private HingeJoint2D _hj;
    [Tooltip("Drag door you want to open here")]
    //[SerializeField] private DoorOpen _doorOpen;

    [SerializeField] private GameObject _affectedObject;
    public AffectedObject _AffectedObject;

    public Animator lever;
    public Animator animator;
    public Animator FanAnimation;
    public Animator BladeAnimation;

    [SerializeField] ParticleSystem _fanParticles = null;

    [SerializeField] private AudioSource _audioSource;
    
    


    public enum AffectedObject
    {
        Door,
        Fan,
        ConveyorBelt
    }

    private void Awake()
    {
        _hj = GetComponent<HingeJoint2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_hj.jointAngle <= -85)
        {
            AffectedObjectOn();
            lever.SetBool("Lever_State", true);
        }
        else if(_hj.jointAngle >= -75)
        {
            AffectedObjectOff();
            lever.SetBool("Lever_State", false);
        }
    }
    
    private void AffectedObjectOn()
    {
        switch (_AffectedObject)
        {
            case AffectedObject.Door:
                var doorOpen = _affectedObject.GetComponent<DoorOpen>();
                doorOpen.OpenDoor();
                break;
            case AffectedObject.Fan:
                var fan = _affectedObject.GetComponent<AreaEffector2D>();
                FanAnimation.SetBool("Fan_State", true);
                BladeAnimation.SetBool("Fan_State", true);
                _fanParticles.Play();
                fan.enabled = true;
                break;
            case AffectedObject.ConveyorBelt:
                var belt = _affectedObject.GetComponent<SurfaceEffector2D>();
                animator.SetBool("PressButton", true);
                belt.enabled = true;
                break;
        }
    }
    
    private void AffectedObjectOff()
    {
        switch (_AffectedObject)
        {
            case AffectedObject.Door:
                var doorOpen = _affectedObject.GetComponent<DoorOpen>();
                doorOpen.CloseDoor();
                break;
            case AffectedObject.Fan:
                var fan = _affectedObject.GetComponent<AreaEffector2D>();
                FanAnimation.SetBool("Fan_State", false);
                BladeAnimation.SetBool("Fan_State", false);
                fan.enabled = false;
                break;
            case AffectedObject.ConveyorBelt:
                var belt = _affectedObject.GetComponent<SurfaceEffector2D>();
                animator.SetBool("PressButton", false);
                belt.enabled = false;
                break;
        }
    }
    public void Collect()
    {
        _fanParticles.Play();
    }
}
