using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public GameObject _virtualCamera;
    public GameObject[] _supportCameras;
    //public GameObject _SupportCameras2;


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
    }

}
