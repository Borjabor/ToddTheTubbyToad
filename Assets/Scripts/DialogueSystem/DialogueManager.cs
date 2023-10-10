using System;
using System.Collections.Generic;
using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Articy.UnityImporterTutorial;
using UnityEngine.Events; //has to be renamed to project used from Articy

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
    {
        private static DialogueManager instance;
        [SerializeField] 
        private GameState _gameState;
        private UnityAction _continueClose;

        [Header("Dialogue Container")]
        [SerializeField] 
        private GameObject _dialoguePanel;
        [SerializeField] 
        private TextMeshProUGUI _dialogueText;
        [SerializeField] 
        private Button _continueButton;
        [SerializeField] 
        private Button _closeButton;
        
        [Header("Speakers")]
        [SerializeField] 
        private Image _speakerImage;
        [SerializeField] 
        private TextMeshProUGUI _speakerName;
    
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
            _continueClose = ContinueDialogue;
            _continueButton.onClick.AddListener(_continueClose); //not working. Perhaps just use Update get inputs
            _dialoguePanel.SetActive(false);
        }

        public static DialogueManager GetInstance()
        {
            return instance;
        }

        private void ContinueDialogue()
        {
            _flowPlayer.Play();
            Debug.Log($"going");
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
            Debug.Log($"exitingDialogue");
        
            DialogueActive = false;
            _dialoguePanel.SetActive(DialogueActive);
            _flowPlayer.FinishCurrentPausedObject();
        }

        public void OnFlowPlayerPaused(IFlowObject aObject)
        {
            _dialogueText.text = string.Empty;
            _speakerName.text = string.Empty;
        
            var objectWithText = aObject as IObjectWithText;
            if (objectWithText != null)
            {
                _dialogueText.text = objectWithText.Text;
            }
        
            var objectWithSpeaker = aObject as IObjectWithSpeaker;
            if (objectWithSpeaker != null)
            {
                var speakerEntity = objectWithSpeaker.Speaker as Entity;
                if (speakerEntity != null)
                {
                    _speakerName.text = speakerEntity.DisplayName;
                    var speakerAsset = (speakerEntity as IObjectWithPreviewImage).PreviewImage.Asset as Asset;
                    if (speakerAsset != null)
                    {
                        _speakerImage.sprite = speakerAsset.LoadAssetAsSprite();
                    }
                }
            }
        }

        public void OnBranchesUpdated(IList<Branch> aBranches)
        {
            bool dialogueIsFinished = true;
            foreach (var branch in aBranches)
            {
                if (branch.Target is IDialogueFragment)
                {
                    dialogueIsFinished = false;
                    // _continueButton.gameObject.SetActive(true);
                    // _closeButton.gameObject.SetActive(false);
                }
            }

            if (dialogueIsFinished)
            {
                _continueClose = ExitDialogue;
                // _continueButton.gameObject.SetActive(false);
                // _closeButton.gameObject.SetActive(true);
            }
            
            // GameObject button = Instantiate(_closeContinueButton, _dialoguePanel.transform);
            // var buttonComptonent = button.GetComponent<Button>();
            // buttonComptonent.onClick.AddListener(ExitDialogue);
        }

        private void ClearAllBranches()
        {
            foreach (Transform child in _dialoguePanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
