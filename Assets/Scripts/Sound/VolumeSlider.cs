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
    private Slider _effectSlider;
    [SerializeField]
    private TMP_Text _masterSliderValue;
    [SerializeField]
    private TMP_Text _musicSliderValue;
    [SerializeField]
    private TMP_Text _effectSliderValue;

    private void Start()
    {
        SoundManager.Instance.ChangeMasterVolume(_masterSlider.value);
        SoundManager.Instance.ChangeMusicVolume(_musicSlider.value);
        SoundManager.Instance.ChangeEffectVolume(_effectSlider.value);
        _masterSliderValue.text = Mathf.Round(_masterSlider.value * 100f).ToString() + " %";
        _musicSliderValue.text = Mathf.Round(_musicSlider.value * 100f).ToString()+ " %";
        _effectSliderValue.text = Mathf.Round(_effectSlider.value * 100f).ToString()+ " %";
        _masterSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
        _musicSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        _effectSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectVolume(val));
        _masterSlider.onValueChanged.AddListener(val => _masterSliderValue.text = Mathf.Round(val*100).ToString()+ " %");
        _musicSlider.onValueChanged.AddListener(val =>_musicSliderValue.text = Mathf.Round(val*100).ToString()+ " %");
        _effectSlider.onValueChanged.AddListener(val => _effectSliderValue.text = Mathf.Round(val*100).ToString()+ " %");
    }
}
