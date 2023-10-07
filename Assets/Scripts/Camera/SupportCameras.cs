using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCameras : MonoBehaviour
{
    public GameObject[] _mainCamera;
    public GameObject _supportingCamera;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _supportingCamera.SetActive(true);

            foreach (var cam in _mainCamera)
            {
                cam.SetActive(false);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _supportingCamera.SetActive(false);
        foreach (var cam in _mainCamera)
        {
            cam.SetActive(true);
        }
    }
}
