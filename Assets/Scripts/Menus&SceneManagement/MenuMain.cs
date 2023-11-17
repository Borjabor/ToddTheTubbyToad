 
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MenuBase
{

    private void Start() {
        Cursor.visible = true;
    }

    public void Continue() {
        _gameState.Value = States.NORMAL;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void NewGame()
    {
        _gameState.Value = States.NORMAL;
        SceneManager.LoadScene("OpeningCutscene");
    }
}
