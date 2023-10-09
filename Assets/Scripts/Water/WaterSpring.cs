using System.Collections;
using System.Collections.Generic;
using Aarthificial.PixelGraphics.Universal;
using UnityEngine;

public class WaterSpring : MonoBehaviour
{
    public float Velocity = 0;
    public float Force = 0;
    public float Height = 0;
    private float _targetHeight =0;
    
    public void WaveSpringUpdate(float _springStiffness, float _dampening)
    {
        Height = transform.localPosition.y;
        //maximum extension
        var x = Height - _targetHeight;
        var loss = -_dampening * Velocity;

        Force = -_springStiffness * x + loss;
        Velocity += Force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y+Velocity, transform.localPosition.z);
    }
    
}
