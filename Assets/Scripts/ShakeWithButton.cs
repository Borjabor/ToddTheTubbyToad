using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeWithButton : MonoBehaviour
{
    [SerializeField]
    private float _shakeTime;
    [SerializeField]
    private float _shakeAmount;
    
    public void Shake()
    {
        CameraManager.Instance.ShakeCamera(_shakeAmount, _shakeTime);
    }
}
