using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin _cameraNoise;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    /* private static CameraShake instance;

     private CinemachineVirtualCamera _cinemachineVirtualCamera;
     private float shakeTimer;

     private void OnEnable()
     {
         if(instance != null) Debug.Log($"There is another camera shake");
         instance = this;
         Debug.Log($"{instance.name}");
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>(); 
     }

     public static CameraShake GetInstance()
     {
         return instance;
     }

     public void ShakeCamera(float intesity, float time)
     {
         CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
             _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

         cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intesity;
         shakeTimer = time;
     }

     private void Update()
     {
         shakeTimer -= Time.deltaTime;
         if(shakeTimer <= 0f)
         {
             CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
             _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

             cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
         }
     }
    */
}
