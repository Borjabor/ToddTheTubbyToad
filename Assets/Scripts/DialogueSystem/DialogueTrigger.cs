using Articy.Unity;
using UnityEngine;

namespace DialogueSystem
{
    [RequireComponent(typeof(ArticyReference))]
    public class DialogueTrigger : MonoBehaviour, IInteractable
    {
        private ArticyObject _dialogue;
        [SerializeField] 
        protected GameState _gameState;
        //[SerializeField] 
        //private GameObject _prompt;
        [SerializeField]
        private SpriteRenderer _prompt;
        [SerializeField]
        private GameObject _cutscene;

        private void Awake()
        {
            _prompt.color = Color.clear;
            //_prompt.SetActive(false);
            _dialogue = GetComponent<ArticyReference>().reference.GetObject();
        }

        public void Interact()
        {
            if(_gameState.Value != States.NORMAL) return;
            DialogueManager.Instance.EnterDialogue(_dialogue);
            if(_cutscene != null) DialogueManager.Instance.SetCutscene(_cutscene);
            //Debug.Log($"interact");
        }

        public void ShowPrompt()
        {
            //_prompt.SetActive(true);
            _prompt.color = Color.white;
        }

        public void HidePrompt()
        {
            //_prompt.SetActive(false);
            _prompt.color = Color.clear;

        }
    }
}
