using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTransitionTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.ChangeMusic(_audioClip);
            gameObject.SetActive(false);
        }
    }
}
