using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] 
    float maxTimeOn;
    [SerializeField] 
    float maxTimeOff;
    private float changeTime = 0;
    private Light2D _light;
    public bool _isOn;

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    void Update()
    {
        if (_light.enabled) _isOn = true;
        if(!_isOn) return;
        if (Time.time > changeTime) {
            _light.enabled = !_light.enabled;
            if (_light.enabled)
            {
                var timeOn = Random.Range(0.01f, maxTimeOn);
                changeTime = Time.time + timeOn;
            } else {
                var timeOff = Random.Range(0.01f, maxTimeOff);
                changeTime = Time.time + timeOff;
            }
        }
    }
}
