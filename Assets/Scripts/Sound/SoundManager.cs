using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource _musicSource, _effectSource;

    protected override void SingletonAwake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public async void PlaySoundWithVolume(AudioClip clip, float volume)
    {
        var currentVolume = _effectSource.volume;
        _effectSource.volume = volume;
        _effectSource.PlayOneShot(clip);
        await Task.Delay(TimeSpan.FromSeconds(clip.length));
        _effectSource.volume = currentVolume;
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicSource.volume = volume;
    }
    
    public void ChangeEffectVolume(float volume)
    {
        _effectSource.volume = volume;
    }
}
