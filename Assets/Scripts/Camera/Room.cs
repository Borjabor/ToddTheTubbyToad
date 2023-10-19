using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField] private GameObject _virtualCamera;
    //[SerializeField] private GameObject[] _supportCameras;
    //[SerializeField] private GameObject _NextRoomCamera;

    private void OnEnable()
    {
        CameraManager.Instance.SetCamera(_virtualCamera);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.SetCamera(_virtualCamera);
        }
    }
    
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player") && !other.isTrigger)
    //     {
    //         _virtualCamera.SetActive(false);
    //     }
    // }

    /*void Update()
    {
        foreach (GameObject go in _supportCameras)
        {
            if (go.activeInHierarchy)
            {
                _virtualCamera.SetActive(false);
            }
        }

        if (_NextRoomCamera.activeInHierarchy)
        {
            _virtualCamera.SetActive(false);
        }
    }*/

}
