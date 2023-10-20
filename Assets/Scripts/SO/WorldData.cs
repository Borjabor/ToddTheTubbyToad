using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Data/WorldData")]
public class WorldData : ScriptableObject
{
    [SerializeField] 
    private List<TrackedObjectData> _trackedObjectDatas;
    
    public Scene _latestScene {get; private set;}
    public Vector2 _latestCheckpointPosition {get; private set;}
    public GameObject _player {get; private set;}

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    public void TrackObjects(TrackedObjectData data)
    {
        _trackedObjectDatas.Add(data);
    }

    public void ClearObjects()
    {
        _trackedObjectDatas.Clear();
    }
    
    public void UpdateCheckpoint(Vector2 position)
    {
        _latestCheckpointPosition = position;
        Debug.Log($"{_latestCheckpointPosition}");
    }

    public void UpdateScene(int index, Scene activeScene)
    {
        if (_latestScene.buildIndex < index) _latestScene = activeScene;
    }

    public async void LoadGame()
    {
        SceneManager.LoadScene(_latestScene.buildIndex);
        await Task.Delay((int) (1000.0f * Time.deltaTime));
        _player.transform.position = _latestCheckpointPosition;
        foreach (TrackedObjectData data in _trackedObjectDatas) {
            foreach (AObjectWithCheckpoint objects in AObjectWithCheckpoint.ObjectsToTrack)
            {
                objects.transform.position = data.Position;
            }
        }
    }
    
    public string GetJSON()
    {
        return JsonUtility.ToJson(this);
    }
}