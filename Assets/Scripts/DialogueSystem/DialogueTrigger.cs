using System;
using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ArticyReference))]
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    private ArticyObject _dialogue;
    [SerializeField] 
    protected GameState _gameState;
    [SerializeField] 
    private GameObject _prompt;


    private void Awake()
    {
        _prompt.SetActive(false);
        _dialogue = GetComponent<ArticyReference>().reference.GetObject();
    }

    public virtual void Interact()
    {
        //if(_gameState.Value is States.DIALOGUE or States.PAUSED) return;
        DialogueManager.GetInstance().EnterDialogue(_dialogue);
        Debug.Log($"interact");
    }

    public void ShowPrompt()
    {
        _prompt.SetActive(true);
    }

    public void HidePrompt()
    {
        _prompt.SetActive(false);
    }
}
