using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameData _gameData = new GameData();
    private IDataService DataService = new JsonDataService();

    [SerializeField]
    private GameObject _player;

    protected override void SingletonAwake()
    {
        //DontDestroyOnLoad(gameObject);
        SaveScene(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) SaveGame();
        if(Input.GetKeyDown(KeyCode.Alpha2)) LoadGame();
        if(Input.GetKeyDown(KeyCode.M)) _player.transform.position = _gameData.LatestCheckpointPosition;
    }

    void OnEnable()
    {
        //SaveScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    private void SaveGame()
    {
        DataService.SaveData("/save-game.json", _gameData, false);
    }

    public void NewGame()
    {
        _gameData.LatestSceneIndex = 2;
        SaveGame();
        SceneManager.LoadScene("Area_1_Overgrown_Greenhouse");
    }

    public async void LoadGame()
    {
        GameData data = DataService.LoadData<GameData>("/save-game.json", false);
        SceneManager.LoadScene(data.LatestSceneIndex);
        await Task.Delay((int) (10000f * Time.deltaTime));
        //_player.transform.position = data.LatestCheckpointPosition;
        _gameData = data;
        Debug.Log($"{_gameData.LatestCheckpointPosition}/{_gameData.LatestSceneIndex}");
    }

    public void SaveCheckpoint(Vector2 position)
    {
        _gameData.LatestCheckpointPosition = position;
        Debug.Log($"{_gameData.LatestCheckpointPosition}");
        SaveGame();
    }

    public void SaveScene(int index)
    {
        //_gameData.UpdateScene(SceneManager.GetActiveScene().buildIndex, SceneManager.GetActiveScene());
        if (_gameData.LatestSceneIndex <= index)
        {
            _gameData.LatestSceneIndex = index;
            Debug.Log($"{_gameData.LatestSceneIndex}");
            SaveGame();
        }
    }
    
}
