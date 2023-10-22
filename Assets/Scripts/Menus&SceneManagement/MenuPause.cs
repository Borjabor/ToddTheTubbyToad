using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MenuBase
{
    private GameState _gameState;

    private bool _isPaused;

    private void Awake()
    {
        _gameState = Resources.Load<GameState>("SOAssets/Game State");
        _thisMenu.SetActive(false);
        _levelLoader = GetComponentInChildren<LevelLoader>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
           _isPaused = !_isPaused;
           PauseGame();
        }
    }

    void PauseGame() {
        if(_isPaused)
        {
            _gameState.Value = States.PAUSED;
            _thisMenu.SetActive(true);
        }
        else 
        {
           _gameState.Value = States.NORMAL;
            _thisMenu.SetActive(false);
        }
    }

    public void ResumeGame() {
        _gameState.Value = States.NORMAL;
       _thisMenu.SetActive(false);
    }

    public void Restart() {
        _gameState.Value = States.NORMAL;
        _thisMenu.SetActive(false);
        _levelLoader.ReloadCurrentLevel(); //Will use GameManager load
    }

}
