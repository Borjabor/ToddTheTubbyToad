using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

public class DangerZone : MonoBehaviour
{
    public static DangerZone Instance { get; private set; }
    
    private float _waitTime = 3f;
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cameraNoise;
    [SerializeField]
    private float _cameraShakeAmount = 2f;
    [SerializeField]
    private CharacterController _player;
    private float _startingZoom;
    private float _currentZoom;
    private float _targetZoom;
    private float _timeElapsed;

    private void Awake()
    {
        Instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _startingZoom = _virtualCamera.m_Lens.OrthographicSize;
        _currentZoom = _startingZoom;
        _targetZoom = _virtualCamera.m_Lens.OrthographicSize * 0.7f;
    }
    
    void Update()
    {
        if (_player.IsSafe)
        {
            Debug.Log($"Safe");
            _virtualCamera.m_Lens.OrthographicSize = _startingZoom;
            _currentZoom = _startingZoom;
            _cameraNoise.m_AmplitudeGain = 0;
            _timeElapsed = 0;
            return;
        }

        _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_currentZoom, _targetZoom, _timeElapsed/_waitTime);
        _cameraNoise.m_AmplitudeGain = Mathf.Lerp(0, _cameraShakeAmount, _timeElapsed / _waitTime);
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= _waitTime)
        {
            _player.Die();
        }
    }
}
