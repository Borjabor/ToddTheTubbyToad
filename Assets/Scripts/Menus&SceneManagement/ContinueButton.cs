using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContinueButton : ButtonSelector
{
    private GameData _gameData = new GameData();
    private IDataService DataService = new JsonDataService();
    protected override async void Awake()
    {
        _button = GetComponent<Button>();
        await Task.Delay((int) (10000f * Time.deltaTime));
        GameData data = DataService.LoadData<GameData>("/save-game.json", false);
        _gameData = data;
        _button.interactable = _gameData.LatestSceneIndex >= 2;
        Debug.Log($"{_gameData.LatestCheckpointPosition}/{_gameData.LatestSceneIndex}");
    }
}
