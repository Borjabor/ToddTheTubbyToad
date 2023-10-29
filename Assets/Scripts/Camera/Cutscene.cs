using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject _player;
    public GameObject _cutsceneCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_player && !other.isTrigger)
        {
            _cutsceneCam.SetActive(true);
        }
    }

}
