using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField] 
    private GameObject _virtualCamera;

    private void OnEnable()
    {
        CameraManager.Instance.SetCamera(_virtualCamera);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        CameraManager.Instance.SetCamera(_virtualCamera);
    }

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.SetCamera(_virtualCamera);
        }
    }*/

}
