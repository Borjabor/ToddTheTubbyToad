 
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MenuBase
{

    private void Start() {
        Cursor.visible = true;
    }

    public void Continue() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Area_1_Overgrown_Greenhouse");
    }
}
