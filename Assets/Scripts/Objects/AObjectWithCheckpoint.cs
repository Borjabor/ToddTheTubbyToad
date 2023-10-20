using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AObjectWithCheckpoint : MonoBehaviour
{
    public static List<AObjectWithCheckpoint> ObjectsToTrack = new List<AObjectWithCheckpoint>();

    private void Awake()
    {
        ObjectsToTrack.Add(this);
    }
}
