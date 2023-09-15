using System;
using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ArticyReference))]
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    protected ArticyObject _dialogue;
    
    [SerializeField] protected GameState _gameState;
    [SerializeField] private GameObject _prompt;
    //[SerializeField] private ArticyRef[] _dialogueRef;
    

    protected void Awake()
    {
        _prompt.SetActive(false);
        _dialogue = GetComponent<ArticyReference>().reference.GetObject();
        //_dialogue = _dialogueRef[2].GetObject();
    }

    public virtual void Interact()
    {
        if(_gameState.Value is States.DIALOGUE or States.PAUSED) return;
        DialogueManager.GetInstance().ClearPlayer();
        DialogueManager.GetInstance().EnterDialogue(_dialogue);
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
