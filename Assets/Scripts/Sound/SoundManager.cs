using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource _musicSource01, _musicSource02;
    private AudioSource _activeMusicSource;

    public AudioMixer _musicMixer, _effectMixer;

    [SerializeField]
    private float _timeToFade = 0.75f;
    private float _timaElapsed;

    private bool _isPlayingTrack01;

    

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.C)) PlayerPrefs.DeleteAll();
    }
    
    protected override void SingletonAwake()
    {
        DontDestroyOnLoad(gameObject);
        _isPlayingTrack01 = true;
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    
    public void ChangeEffectsVolume(float volume)
    {
        _effectMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
    }
    
    public void ChangeMusic(AudioClip clip)
    {
        StopAllCoroutines();
        StartCoroutine(CrossFade(clip));
    }

    private IEnumerator CrossFade(AudioClip clip)
    {
        if (_isPlayingTrack01)
        {
            _musicSource02.clip = clip;
            _musicSource02.Play();
            while (_timaElapsed < _timeToFade)
            {
                _musicSource02.volume = Mathf.Lerp(0, 1, _timaElapsed / _timeToFade);
                _musicSource01.volume = Mathf.Lerp(1, 0, _timaElapsed / _timeToFade);
                _timaElapsed += Time.deltaTime;
                yield return null;
            }
            _musicSource01.Stop();
        }
        else
        {
            _musicSource01.clip = clip;
            _musicSource01.Play();
            while (_timaElapsed < _timeToFade)
            {
                _musicSource01.volume = Mathf.Lerp(0, 1, _timaElapsed / _timeToFade);
                _musicSource02.volume = Mathf.Lerp(1, 0, _timaElapsed / _timeToFade);
                _timaElapsed += Time.deltaTime;
                yield return null;
            }
            _musicSource02.Stop();
        }
        
    }
}
