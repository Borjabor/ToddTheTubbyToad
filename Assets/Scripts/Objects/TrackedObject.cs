using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedObject : AObjectWithCheckpoint
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("ObjectCheckpoint")) return;
        //GameManager.Instance.SaveTrackedObjects();
    }
}
