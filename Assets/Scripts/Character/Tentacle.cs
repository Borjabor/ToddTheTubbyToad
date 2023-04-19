using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int _length;
    public LineRenderer _lineRend;
    public Vector3[] _segmentPoses;
    private Vector3[] _segmentV;
    private Vector3 _offset;

    public Transform _targetDir;
    public float _targetDist;
    public float _smoothSpeed;
    public float _trailSpeed;
    public static GameObject GrabbedObject;
    

    void Start()
    {
        _lineRend.positionCount = +_length;
        _segmentPoses = new Vector3[_length];
        _segmentV = new Vector3[_length];
    }

    void Update()
    {
        _segmentPoses[0] = _targetDir.position;

        for (int i = 1; i < _segmentPoses.Length; i++)
        {
            if (GrabbedObject == null)
            {
                _segmentPoses[i] = Vector3.SmoothDamp(_segmentPoses[i], _segmentPoses[i - 1] +_targetDir.right * _targetDist, ref _segmentV[i], _smoothSpeed + i / _trailSpeed);
            }
            else
            {
                _offset = _targetDir.position + (GrabbedObject.transform.position - _targetDir.position)/2;
                _segmentPoses[i] = _offset;
            }
        }
        _lineRend.SetPositions(_segmentPoses);
    }
}
