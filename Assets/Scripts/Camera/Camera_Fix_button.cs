using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Fix_button : MonoBehaviour
{
    [SerializeField] private GameObject[] _supportCameras;

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject go in _supportCameras)
        {
            if(Input.GetKeyDown("c"))
            {
                _supportCameras.SetActive(false);
            }
        }
    }
}
