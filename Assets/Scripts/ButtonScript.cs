using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private Vector3 _oringialPos;
    [Tooltip("Drag button here")]
    [SerializeField] private GameObject _targetPos;
    bool moveBack = false;
    [Tooltip("Drag door you want to open here")]
    //[SerializeField] private DoorOpen _doorOpen;
    [SerializeField] private GameObject _affectedObject;
    public AffectedObject _AffectedObject;

    public Animator button;
    public Animator animator;

    public Animator FanAnimation;
    public Animator BladeAnimation;

    [SerializeField] ParticleSystem _fanParticles;



    public enum AffectedObject
    {
        Door,
        Fan,
        ConveyorBelt
    }
    private void Start()
    {
        _oringialPos = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Object")
        {
            //transform.Translate(0, -0.01f, 0);
            transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, Time.deltaTime);
            button.SetBool("Button_State", true);
            moveBack = false;
            AffectedObjectOn();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Object")
        {
            collision.transform.parent = transform;
        }
        button.SetBool("Button_State", false);
        moveBack = true;
        collision.transform.parent = null;
        AffectedObjectOff();
    }

    private void Update()
    {
        if (moveBack)
        {
            if (transform.position.y < _oringialPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                moveBack = false;
            }
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
                if (!_fanParticles.isPlaying) _fanParticles.Play();
                fan.enabled = true;
                if (FanAnimation.isInitialized) _fanParticles.Play();
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
                if (_fanParticles.isPlaying) _fanParticles.Stop();
                fan.enabled = false;
                break;
            case AffectedObject.ConveyorBelt:
                var belt = _affectedObject.GetComponent<SurfaceEffector2D>();
                animator.SetBool("PressButton", false);
                belt.enabled = false;
                break;
        }
    }
}
