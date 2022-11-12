using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] 
    private LevelLoader _levelLoader;
    
    [SerializeField]
    private GameObject _pauseMenu;

    private bool _isPaused;

    private void Awake()
    {
        _levelLoader = GetComponentInChildren<LevelLoader>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
           _isPaused = !_isPaused;
           PauseGame();
        }

        if(!_isPaused) {
            Cursor.visible = false;
        }
    }

    void PauseGame() {
        if(_isPaused)
        {
            Time.timeScale = 0f;
             _pauseMenu.SetActive(true);
             Cursor.visible = true;
        }
        else 
        {
            Time.timeScale = 1;
             _pauseMenu.SetActive(false);
        }
    }

    public void ResumeGame() {
       Time.timeScale = 1;
       Cursor.visible = false;
       _pauseMenu.SetActive(false);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit() {
        Application.Quit();
    }

    public void Restart() {
        Time.timeScale = 1;
        Cursor.visible = false;
        _pauseMenu.SetActive(false);
        _levelLoader.ReloadCurrentLevel();
    }

}
