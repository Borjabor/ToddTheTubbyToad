using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [HideInInspector]
    public static bool _isHolding = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _isHolding = true;
        }
        else
        {
            _isHolding = false;
            Tentacle.GrabbedObject = null;
            Destroy(GetComponent<FixedJoint2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isHolding && other.gameObject.CompareTag("Object"))
        {
            Rigidbody2D rb = other.transform.GetComponent<Rigidbody2D>();
            Tentacle.GrabbedObject = other.gameObject;
            if (rb != null)
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                fj.connectedBody = rb;
            }
            else
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            }
        }
    }
}
