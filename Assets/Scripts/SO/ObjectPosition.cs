using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Data/ObjectData")]
public class ObjectPosition : TAsset<Vector2>
{
    [SerializeField]
    private GameObject _originalPosition;
    [SerializeField]
    private GameObject _newPosition;

    private void OnValidate()
    {
        _value = _originalPosition.transform.position;
    }

    public void NewPosition()
    {
        _value  = _newPosition.transform.position;
    }
    
}
