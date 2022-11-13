 
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start() {
        Cursor.visible = true;
    }

    public void StartGame() {
        CollectiblesCounter.TotalPoints = 0;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void Exit() {
        Application.Quit();
    }
}
