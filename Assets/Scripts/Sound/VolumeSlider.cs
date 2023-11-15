using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _masterSlider;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _effectsSlider;
    [SerializeField]
    private TMP_Text _masterSliderValue;
    [SerializeField]
    private TMP_Text _musicSliderValue;
    [SerializeField]
    private TMP_Text _effectSliderValue;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            var value = PlayerPrefs.GetFloat("masterVolume");
            _masterSlider.value = value;
            SoundManager.Instance.ChangeMasterVolume(value);
        }
        else
        {
            _masterSlider.value = 1;
            SoundManager.Instance.ChangeMasterVolume(1);
        }
        
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            var value = PlayerPrefs.GetFloat("musicVolume");
            _musicSlider.value = value;
            SoundManager.Instance.ChangeMusicVolume(value);
        }
        else
        {
            _musicSlider.value = 1;
            SoundManager.Instance.ChangeMusicVolume(1);
        }
        
        if (PlayerPrefs.HasKey("effectsVolume"))
        {
            var value = PlayerPrefs.GetFloat("effectsVolume");
            _effectsSlider.value = value;
            SoundManager.Instance.ChangeEffectsVolume(value);
        }
        else
        {
            _masterSlider.value = 1;
            SoundManager.Instance.ChangeEffectsVolume(1);
        }
        
        _masterSliderValue.text = Mathf.Round(_masterSlider.value * 100f).ToString() + " %";
        _musicSliderValue.text = Mathf.Round(_musicSlider.value * 100f).ToString()+ " %";
        _effectSliderValue.text = Mathf.Round(_effectsSlider.value * 100f).ToString()+ " %";
        
        _masterSlider.onValueChanged.AddListener(val =>
        {
            SoundManager.Instance.ChangeMasterVolume(val);
            _masterSliderValue.text = Mathf.Round(val * 100).ToString() + " %";
            PlayerPrefs.SetFloat("masterVolume", val);
            PlayerPrefs.Save();
        });
        _musicSlider.onValueChanged.AddListener(val =>
        {
            SoundManager.Instance.ChangeMusicVolume(val);
            _musicSliderValue.text = Mathf.Round(val * 100).ToString() + " %";
            PlayerPrefs.SetFloat("musicVolume", val);
            PlayerPrefs.Save();
        });
        _effectsSlider.onValueChanged.AddListener(val =>
        {
            SoundManager.Instance.ChangeEffectsVolume(val);
            _effectSliderValue.text = Mathf.Round(val * 100).ToString() + " %";
            PlayerPrefs.SetFloat("effectsVolume", val);
            PlayerPrefs.Save();
        });
    }
}
