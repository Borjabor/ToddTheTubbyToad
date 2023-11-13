using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource _musicSource, _effectSource;

    [SerializeField]
    private AudioMixer _musicMixer, _effectMixer;

    

    protected override void SingletonAwake()
    {
        DontDestroyOnLoad(this);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicSource.volume = volume;
        _musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    
    public void ChangeEffectVolume(float volume)
    {
        _effectSource.volume = volume;
        _effectMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
    }
}
