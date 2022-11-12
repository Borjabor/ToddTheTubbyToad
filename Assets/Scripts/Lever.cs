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


    public enum AffectedObject
    {
        Door,
        Fan,
        ConveyorBelt
    }

    private void Awake()
    {
        _hj = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        if (_hj.jointAngle <= -85)
        {
            AffectedObjectOn();
        }
        else
        {
            AffectedObjectOff();
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
                fan.enabled = true;
                break;
            case AffectedObject.ConveyorBelt:
                var belt = _affectedObject.GetComponent<SurfaceEffector2D>();
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
                fan.enabled = false;
                break;
            case AffectedObject.ConveyorBelt:
                var belt = _affectedObject.GetComponent<SurfaceEffector2D>();
                belt.enabled = false;
                break;
        }
    }
}
