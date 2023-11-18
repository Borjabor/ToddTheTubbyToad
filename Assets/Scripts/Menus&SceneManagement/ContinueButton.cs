using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContinueButton : ButtonSelector
{
    private GameData _gameData = new GameData();
    private IDataService DataService = new JsonDataService();
    protected override void Awake()
    {
        _button = GetComponent<Button>();
        GameData data = DataService.LoadData<GameData>("/save-game.json", false);
        _gameData = data;
        _button.interactable = _gameData.LatestSceneIndex >= 2;
        Debug.Log($"{_gameData.LatestCheckpointPosition}/{_gameData.LatestSceneIndex}");
    }
}
