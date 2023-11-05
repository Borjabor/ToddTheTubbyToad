using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class TriggerZoneDialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueTrigger _character;

    private GameObject _npc;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _npc = _character.gameObject;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        var positionX = _npc.transform.position.x - other.gameObject.transform.position.x > 0 ? 1 : -1;
        var positionY = _camera.transform.position.y - other.gameObject.transform.position.y > 0 ? 1 : -1;
        DialogueManager.Instance.GetPlayer(positionX, positionY);
        _character.Interact();
        gameObject.SetActive(false);
    }
}
