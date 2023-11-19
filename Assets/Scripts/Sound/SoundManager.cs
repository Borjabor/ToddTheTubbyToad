using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource _musicSource01, _musicSource02;
    private AudioSource _activeMusicSource;
    private float _source01Volume, _source02Volume;

    public AudioMixer _musicMixer, _effectMixer;
    
    private float _timeElapsed;

    private bool _isPlayingTrack01;
    
    protected override void SingletonAwake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private async void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        await Task.Delay((int) (10000f * Time.deltaTime));
        
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            var value = PlayerPrefs.GetFloat("masterVolume");
            ChangeMasterVolume(value);
        }
        else
        {
            ChangeMasterVolume(1);
        }
        
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            var value = PlayerPrefs.GetFloat("musicVolume");
            ChangeMusicVolume(value);
        }
        else
        {
            ChangeMusicVolume(1);
        }
        
        if (PlayerPrefs.HasKey("effectsVolume"))
        {
            var value = PlayerPrefs.GetFloat("effectsVolume");
            ChangeEffectsVolume(value);
        }
        else
        {
            ChangeEffectsVolume(1);
        }
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
    
    public void ChangeMusic(AudioClip clip, float timeToFade)
    {
        StopAllCoroutines();
        StartCoroutine(CrossFade(clip, timeToFade));
    }

    private IEnumerator CrossFade(AudioClip clip, float timeToFade)
    {
        if (_musicSource01.isPlaying)
        {
            _musicSource02.clip = clip;
            _musicSource02.Play();
            while (_timeElapsed < timeToFade)
            {
                _musicSource02.volume = Mathf.Lerp(0, 1, _timeElapsed / timeToFade);
                _musicSource01.volume = Mathf.Lerp(1, 0, _timeElapsed / timeToFade);
                _timeElapsed += Time.deltaTime;
                yield return null;
            }
            _musicSource01.Stop();
        }
        else
        {
            _musicSource01.clip = clip;
            _musicSource01.Play();
            while (_timeElapsed < timeToFade)
            {
                _musicSource01.volume = Mathf.Lerp(0, 1, _timeElapsed / timeToFade);
                _musicSource02.volume = Mathf.Lerp(1, 0, _timeElapsed / timeToFade);
                _timeElapsed += Time.deltaTime;
                yield return null;
            }
            _musicSource02.Stop();
        }
        
    }
}
