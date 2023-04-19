using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Hook();
        }
    }

    void Hook()
    {
        animator.SetTrigger("Hook");
    }

}
