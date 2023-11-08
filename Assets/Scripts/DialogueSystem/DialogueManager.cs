using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Articy.Todd_The_Tubby_Toad;

namespace DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>, IArticyFlowPlayerCallbacks
    {
        private static DialogueManager instance;
        [SerializeField] 
        private GameState _gameState;
        [SerializeField]
        private float _typingSpeed = 0.02f;
        private Coroutine _coroutine;
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
        private GameObject _continueCloseButtonObject;
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
        private GameObject _cutscene;

        protected override void SingletonAwake()
        {
        }

        private void Start()
        {
            _isPlayer = false;
            _flowPlayer = GetComponent<ArticyFlowPlayer>();
            _dialoguePanelTransform = _dialoguePanel.GetComponent<RectTransform>();
            _dialoguePanel.SetActive(false);
        }

        public void SetCutscene(GameObject cutsceneTrigger)
        {
            _cutscene = cutsceneTrigger;
        }

        private void ContinueDialogue()
        {
            _flowPlayer.Play();
        }
        
        public async void EnterDialogue(IArticyObject aObject)
        {
            _gameState.Value = States.DIALOGUE;
            DialogueActive = true;
            _dialoguePanel.SetActive(DialogueActive);
            await Task.Delay((int) (1000.0f * Time.deltaTime));
            _continueCloseButton.Select();
            _dialogueText.text = string.Empty;
            _continueCloseButton.onClick.RemoveListener(ExitDialogue);
            _continueCloseButton.onClick.AddListener(ContinueDialogue);
            _buttonText.text = "Continue";
            _dialoguePanelTransform.localPosition = new Vector3(0, 340 * _playerPositionY, 0);
            _flowPlayer.StartOn = aObject;
        }

        private async void ExitDialogue()
        {
            DialogueActive = false;
            _dialoguePanel.SetActive(DialogueActive);
            _flowPlayer.FinishCurrentPausedObject();
            await Task.Delay(TimeSpan.FromSeconds(1f));
            if (_cutscene != null)
            {
                _cutscene.SetActive(true);
                _cutscene = null;
            }
            _gameState.Value = States.NORMAL;
        }

        public void OnFlowPlayerPaused(IFlowObject aObject)
        {
            _dialogueText.text = string.Empty;
            _speakerName.text = string.Empty;

            //if (aObject is IObjectWithText objectWithText) _dialogueText.text = objectWithText.Text;
            if(_coroutine != null) StopCoroutine(_coroutine);
            if (aObject is IObjectWithText objectWithText) _coroutine = StartCoroutine(DisplayLine(objectWithText.Text));

            if (aObject is not IObjectWithSpeaker objectWithSpeaker) return;
            var speakerEntity = objectWithSpeaker.Speaker as Entity;
            if (speakerEntity == null) return;
            _speakerName.text = speakerEntity.DisplayName;
            _speakerContainer.localPosition = speakerEntity.DisplayName == _playerArticyTag ? new Vector3(796 * -_playerPositionX, 0, 0) : new Vector3(796 * _playerPositionX, 0, 0);
            var speakerAsset = (speakerEntity as IObjectWithPreviewImage).PreviewImage.Asset as Asset;
            if (speakerAsset != null) _speakerImage.sprite = speakerAsset.LoadAssetAsSprite();
        }

        private IEnumerator DisplayLine(string line)
        {
            _continueCloseButtonObject.SetActive(false);
            _dialogueText.text = "";

            foreach (char letter in line)
            {
                // if (Input.GetKeyDown(KeyCode.Space))
                // {
                //     Debug.Log($"press");
                //     _dialogueText.text = line;
                //     break;
                // }  
                _dialogueText.text += letter;
                yield return new WaitForSeconds(_typingSpeed);
            }

            _continueCloseButtonObject.SetActive(true);
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
