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

    private async void OnEnable()
    {
        await Task.Delay((int) (1000.0f * Time.deltaTime));
        _startButton.Select();
    }
    
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsMenu()
    {
        _thisMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
