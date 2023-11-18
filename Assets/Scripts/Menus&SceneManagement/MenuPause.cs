using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MenuBase
{
    private bool _isPaused;

    private void Start()
    {
        _thisMenu.SetActive(false);
        _levelLoader = GetComponentInChildren<LevelLoader>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(_gameState.Value == States.PAUSED || _gameState.Value == States.NORMAL) PauseGame();
        }
    }

    public void PauseGame() {
        _isPaused = !_isPaused;
        if(_isPaused)
        {
            _gameState.Value = States.PAUSED;
            _thisMenu.SetActive(true);
        }
        else 
        {
           _gameState.Value = States.NORMAL;
            _thisMenu.SetActive(false);
            _settingsMenu.SetActive(false);
        }
    }

    public void Restart() {
        _gameState.Value = States.NORMAL;
        _thisMenu.SetActive(false);
        _levelLoader.ReloadCurrentLevel(); //Will use GameManager load
    }

}
