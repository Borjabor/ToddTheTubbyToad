using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MenuBase
{
    [SerializeField]
    private Button _previousMenuButton;
    
    public async void Return()
    {
        _settingsMenu.SetActive(false);
        _thisMenu.SetActive(true);
        await Task.Delay((int) (1000.0f * Time.deltaTime));
        _previousMenuButton.Select();
    }

    
}
