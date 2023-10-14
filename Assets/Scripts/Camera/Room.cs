using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField] private GameObject _virtualCamera;
    [SerializeField] private GameObject[] _supportCameras;
    [SerializeField] private GameObject _NextRoomCamera;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _virtualCamera.SetActive(true);

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _virtualCamera.SetActive(false);
      
        }
    }

    void Update()
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
    }

}
