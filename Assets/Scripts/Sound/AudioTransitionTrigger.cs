using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AudioTransitionTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private float _timeToFade;

    [SerializeField]
    private bool _workOnAwake;

    private void Awake()
    {
        if(_workOnAwake) MusicChange();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MusicChange();
        }
    }

    private async void MusicChange()
    {
        await Task.Delay((int) (5000f * Time.deltaTime));
        SoundManager.Instance.ChangeMusic(_audioClip, _timeToFade);
        gameObject.SetActive(false);
    }
}
