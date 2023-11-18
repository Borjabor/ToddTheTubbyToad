using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    private GameObject _currentVirtualCamera;
    [SerializeField]
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField]
    private CinemachineBasicMultiChannelPerlin _cameraNoise;
    private CharacterController _player;
    private float _shakeTimer;
    [SerializeField]
    private float _timeToDeath = 3f;
    [SerializeField]
    private float _cameraShakeAmount = 2f;
    private float _startingZoom;
    private float _currentZoom;
    private float _targetZoom;
    private float _timeElapsed;
    
    protected override void SingletonAwake()
    {
        
    }
    
    public void SetCamera(GameObject virtualCamera)
    {
        if (_currentVirtualCamera != null) _currentVirtualCamera.SetActive(false);
        _currentVirtualCamera = virtualCamera;
        _currentVirtualCamera.SetActive(true);
        _cinemachineVirtualCamera = _currentVirtualCamera.GetComponent<CinemachineVirtualCamera>();
        _cameraNoise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _startingZoom = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
        _currentZoom = _startingZoom;
        _targetZoom = _cinemachineVirtualCamera.m_Lens.OrthographicSize * 0.7f;
    }

    public void SetPlayer(CharacterController player)
    {
        _player = player;
    }

    public void ShakeCamera(float intensity, float time)
    { 
        _cameraNoise.m_AmplitudeGain = intensity;
        _shakeTimer = time;
    }

    private async void Update()
    {
        await Task.Delay((int) (1000.0f * Time.deltaTime));
        _shakeTimer -= Time.deltaTime;
        if(_shakeTimer > 0f) return;
        if(_cinemachineVirtualCamera !=null) _cameraNoise.m_AmplitudeGain = 0;
        _timeElapsed = 0;
        
        // if (_player.IsSafe)
        // {
        //     _cinemachineVirtualCamera.m_Lens.OrthographicSize = _startingZoom;
        //     _currentZoom = _startingZoom;
        //     _cameraNoise.m_AmplitudeGain = 0;
        //     _timeElapsed = 0;
        //     return;
        // }
        //
        // _cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_currentZoom, _targetZoom, _timeElapsed/_timeToDeath);
        // _cameraNoise.m_AmplitudeGain = Mathf.Lerp(0, _cameraShakeAmount, _timeElapsed / _timeToDeath);
        // _timeElapsed += Time.deltaTime;
        // if (_timeElapsed >= _timeToDeath)
        // {
        //     _player.Die();
        // }
    }

}
