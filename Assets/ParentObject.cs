using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObject : MonoBehaviour
{

    public bool _platform = false;
    public GameObject _myObject;


    private void Update()
    {
        if (_platform)
        {
            _myObject.transform.SetParent(this.transform);
        }
        else
        {
            _myObject.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            _platform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            _platform = false;
        }
    }
}
