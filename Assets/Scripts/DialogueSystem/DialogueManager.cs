using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Unity.Interfaces;
//using Articy.Stygian_Crossing; //has to be renamed to project used from Articy

public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    private static DialogueManager instance;
    [SerializeField] private GameState _gameState;

    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private RectTransform _choicesPanel;
    [SerializeField] private GameObject _choicesPrefab;
    [SerializeField] private GameObject _closePrefab;
    [Header("Speakers")]
    [SerializeField] private Image _speakerImage;
    [SerializeField] private TextMeshProUGUI _speakerName;
    [SerializeField] private Image _playerImage;
    [SerializeField] private TextMeshProUGUI _playerName;
    

    
    private bool _isPlayer;
    public bool DialogueActive { get; set; }

    private ArticyFlowPlayer _flowPlayer;
    private ArticyObject _currentDialogue;


    private void Awake()
    {
        if(instance != null) Debug.Log($"There is another Dialogue Manager");
        instance = this;
    }

    private void Start()
    {
        _isPlayer = false;
        _flowPlayer = GetComponent<ArticyFlowPlayer>();
        _dialoguePanel.SetActive(false);
    }
    
    public static DialogueManager GetInstance()
    {
        return instance;
    }



    private void Update()
    {
        //if(_gameState.Value is States.DIALOGUE or States.PAUSED) return;
    }

    public void EnterDialogue(IArticyObject aObject)
    {
        _gameState.Value = States.DIALOGUE;
        _dialogueText.text = string.Empty;
        DialogueActive = true;
        _dialoguePanel.SetActive(DialogueActive);
        _flowPlayer.StartOn = aObject;
    }

    public void ExitDialogue()
    {
        _gameState.Value = States.NORMAL;
        
        DialogueActive = false;
        _dialoguePanel.SetActive(DialogueActive);
        _flowPlayer.FinishCurrentPausedObject();
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
    //     _dialogueText.text = string.Empty;
    //     _speakerName.text = string.Empty;
    //     
    //     
    //     var objectWithSpeaker = aObject as IObjectWithSpeaker;
    //     var speakerEntity = objectWithSpeaker.Speaker as Entity;
    //     if (objectWithSpeaker != null)
    //     {
    //         if (speakerEntity != null) _speakerName.text = speakerEntity.DisplayName;
    //         if (speakerEntity != null)
    //         {
    //             _speakerName.text = speakerEntity.DisplayName;
    //             var speakerAsset = ((speakerEntity as IObjectWithPreviewImage).PreviewImage.Asset as Asset);
    //             if (speakerAsset != null)
    //             {
    //                 _speakerImage.sprite = speakerAsset.LoadAssetAsSprite();
    //             }
    //         }
    //     }
    //     
    //     var objectWithText = aObject as IObjectWithText;
    //     if (objectWithText != null)
    //     {
    //         _dialogueText.text = objectWithText.Text;
    //     }
    //     
    //     var dialogueSpeaker = aObject as IObjectWithSpeaker;
    //     if (dialogueSpeaker != null)
    //     {
    //         var speaker = dialogueSpeaker.Speaker;
    //         if (speaker != null)
    //         {
    //             var speakerAsset = ((speaker as IObjectWithPreviewImage).PreviewImage.Asset as Asset);
    //             if (speakerAsset != null)
    //             {
    //                 _speakerImage.sprite = speakerAsset.LoadAssetAsSprite();
    //             }
    //         }
    //     }
    //     
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        ClearAllBranches();
        
        bool dialogueIsFinished = true;
        foreach (var branch in aBranches)
        {
            if (branch.Target is IDialogueFragment)
            {
                dialogueIsFinished = false;
            }
        }

        if (!dialogueIsFinished)
        {
            foreach (var branch in aBranches)
            {
                GameObject button = Instantiate(_choicesPrefab, _choicesPanel);
                button.GetComponent<BranchChoice>().AssignBranch(_flowPlayer, branch);
            }
        }
        else
        {
            GameObject button = Instantiate(_closePrefab, _choicesPanel);
            var buttonComptonent = button.GetComponent<Button>();
            buttonComptonent.onClick.AddListener(ExitDialogue);
        }
    }

    private void ClearAllBranches()
    {
        foreach (Transform child in _choicesPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
