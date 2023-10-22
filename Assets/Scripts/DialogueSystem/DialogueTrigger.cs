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
        [SerializeField] 
        private GameObject _prompt;
        [SerializeField]
        private GameObject _cutscene;

        


        private void Awake()
        {
            _prompt.SetActive(false);
            _dialogue = GetComponent<ArticyReference>().reference.GetObject();
        }

        public void Interact()
        {
            if(_gameState.Value != States.NORMAL) return;
            DialogueManager.GetInstance().EnterDialogue(_dialogue);
            if(_cutscene != null) DialogueManager.GetInstance().SetCutscene(_cutscene);
            //Debug.Log($"interact");
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
}
