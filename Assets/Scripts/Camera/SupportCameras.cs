using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCameras : MonoBehaviour
{
    public GameObject _mainCamera;
    public GameObject _supportingCamera;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _supportingCamera.SetActive(true);
            _mainCamera.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _supportingCamera.SetActive(true);
            _mainCamera.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _supportingCamera.SetActive(false);
        _mainCamera.SetActive(true);
    }
}
