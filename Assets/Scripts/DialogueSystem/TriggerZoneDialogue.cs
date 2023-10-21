using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class TriggerZoneDialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueTrigger _character;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        _character.Interact();
    }
}
