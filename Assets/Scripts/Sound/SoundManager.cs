using System.Collections;
using System.Collections.Generic;
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
