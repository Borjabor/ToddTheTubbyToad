using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private float _playerPositionX;
        private float _playerPositionY;

        [Header("Dialogue Container")]
        [SerializeField] 
        private GameObject _dialoguePanel;
        private RectTransform _dialoguePanelTransform;
        [SerializeField] 
        private TextMeshProUGUI _dialogueText;
        [SerializeField] 
        private Button _continueCloseButton;
        [SerializeField] 
        private TextMeshProUGUI _buttonText;
        
        [Header("Speakers")]
        [SerializeField] 
        private Image _speakerImage;
        [SerializeField] 
        private TextMeshProUGUI _speakerName;
        [SerializeField]
        private RectTransform _speakerContainer;
        [SerializeField]
        private string _playerArticyTag;
        
    
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
            _dialoguePanelTransform = _dialoguePanel.GetComponent<RectTransform>();
            _dialoguePanel.SetActive(false);
        }

        public static DialogueManager GetInstance()
        {
            return instance;
        }

        private void ContinueDialogue()
        {
            _flowPlayer.Play();
        }
        
        public void EnterDialogue(IArticyObject aObject)
        {
            _gameState.Value = States.DIALOGUE;
            _dialogueText.text = string.Empty;
            _continueCloseButton.onClick.RemoveListener(ExitDialogue);
            _continueCloseButton.onClick.AddListener(ContinueDialogue);
            _buttonText.text = "Continue";
            _dialoguePanelTransform.localPosition = new Vector3(0, 340 * _playerPositionY, 0);
            DialogueActive = true;
            _dialoguePanel.SetActive(DialogueActive);
            _flowPlayer.StartOn = aObject;
        }

        private async void ExitDialogue()
        {
            DialogueActive = false;
            _dialoguePanel.SetActive(DialogueActive);
            _flowPlayer.FinishCurrentPausedObject();
            await Task.Delay(TimeSpan.FromSeconds(1f));
            _gameState.Value = States.NORMAL;
        }

        public void OnFlowPlayerPaused(IFlowObject aObject)
        {
            _dialogueText.text = string.Empty;
            _speakerName.text = string.Empty;

            if (aObject is IObjectWithText objectWithText) _dialogueText.text = objectWithText.Text;

            if (aObject is not IObjectWithSpeaker objectWithSpeaker) return;
            var speakerEntity = objectWithSpeaker.Speaker as Entity;
            if (speakerEntity == null) return;
            _speakerName.text = speakerEntity.DisplayName;
            _speakerContainer.localPosition = speakerEntity.DisplayName == _playerArticyTag ? new Vector3(796 * -_playerPositionX, 0, 0) : new Vector3(796 * _playerPositionX, 0, 0);
            var speakerAsset = (speakerEntity as IObjectWithPreviewImage).PreviewImage.Asset as Asset;
            if (speakerAsset != null) _speakerImage.sprite = speakerAsset.LoadAssetAsSprite();
        }

        public void OnBranchesUpdated(IList<Branch> aBranches)
        {
            bool dialogueIsFinished = true;
            foreach (var branch in aBranches)
            {
                if (branch.Target is IDialogueFragment) dialogueIsFinished = false;
            }

            if (!dialogueIsFinished) return;
            _continueCloseButton.onClick.RemoveListener(ContinueDialogue);
            _continueCloseButton.onClick.AddListener(ExitDialogue);
            _buttonText.text = "Close";
        }

        public void GetPlayer(float playerPositionX,float playerPositionY)
        {
            _playerPositionX = playerPositionX;
            _playerPositionY = playerPositionY;
        }
    }
}
