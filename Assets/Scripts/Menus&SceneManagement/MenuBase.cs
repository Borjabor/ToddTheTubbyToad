using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject _thisMenu;
    [SerializeField]
    protected GameObject _settingsMenu;
    [SerializeField]
    protected Button _startButton;
    [SerializeField] 
    protected LevelLoader _levelLoader;
    protected GameState _gameState;

    protected virtual void Awake()
    {
        _gameState = Resources.Load<GameState>("SOAssets/Game State");
    }

    private async void OnEnable()
    {
        await Task.Delay((int) (1000.0f * Time.deltaTime));
        _startButton.Select();
    }

    public void MainMenu() {
        _gameState.Value = States.NORMAL;
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsMenu()
    {
        _thisMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void LevelSelect()
    {
        _gameState.Value = States.NORMAL;
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
