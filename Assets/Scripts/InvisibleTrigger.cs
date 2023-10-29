using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MonoBehaviour
{
    [SerializeReference]
    private GameObject _affectedObject;
    private IOnOffObjects _affectedObjectI;

    private void Awake()
    {
        _affectedObjectI = _affectedObject.GetComponent<IOnOffObjects>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _affectedObjectI.TurnOn();
    }
}
