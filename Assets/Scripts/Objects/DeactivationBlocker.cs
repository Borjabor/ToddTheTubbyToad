using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivationBlocker : MonoBehaviour
{
    public GameObject _iceFrog;
    public GameObject _blocker;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            _blocker.SetActive(false);
        }
    }

}
