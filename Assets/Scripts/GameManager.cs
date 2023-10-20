using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private WorldData _worldData;

    [SerializeField]
    private GameObject _player;

    protected override void SingletonAwake()
    {
    }
    
    void OnEnable()
    {
        SaveScene();
        _worldData.SetPlayer(_player);
    }

    private void Start()
    {
        //Debug.Log($"Current Scene: {_worldData._latestScene.name} / Current Checkpoint: {_worldData._latestCheckpointPosition} / Player: {_worldData._player.name}");
    }

    public async void Load()
    {
        //await Task.Delay((int) (1000.0f * Time.deltaTime));
        _worldData.LoadGame();
    }

    public void SaveCheckpoint(Vector2 position)
    {
        _worldData.UpdateCheckpoint(position);
    }

    public void SaveScene()
    {
        _worldData.UpdateScene(SceneManager.GetActiveScene().buildIndex, SceneManager.GetActiveScene());
    }

    public void SaveTrackedObjects()
    {
        _worldData.ClearObjects();
        foreach (AObjectWithCheckpoint objects in AObjectWithCheckpoint.ObjectsToTrack)
        {
            TrackedObjectData data = new TrackedObjectData()
            {
                Name = objects.name,
                Position = objects.transform.position
            };
            _worldData.TrackObjects(data );
        }
    }
    
}
