 
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MenuBase
{

    private void Start() {
        Cursor.visible = true;
    }

    public void Continue() {
        _gameState.Value = States.NORMAL;
        GameManager.Instance.LoadGame();
    }

    public void NewGame()
    {
        _gameState.Value = States.NORMAL;
        GameManager.Instance.NewGame();
    }
}
