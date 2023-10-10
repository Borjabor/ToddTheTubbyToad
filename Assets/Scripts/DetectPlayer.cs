 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private IInteractable _npc;

    private void Awake()
    {
        _npc = GetComponentInParent<IInteractable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) _npc.ShowPrompt();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _npc.HidePrompt();
    }
}
