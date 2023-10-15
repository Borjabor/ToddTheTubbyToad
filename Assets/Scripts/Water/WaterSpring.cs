using System.Collections;
using System.Collections.Generic;
using Aarthificial.PixelGraphics.Universal;
using UnityEngine;
using UnityEngine.U2D;

public class WaterSpring : MonoBehaviour
{
    public float Velocity = 0;
    public float Force = 0;
    // current height
    public float Height = 0;
    // normal height
    private float _targetHeight =0;

    public Transform SpringTransform;
    [SerializeField]
    private static SpriteShapeController _spriteShapeController = null;
    private int waveIndex = 0;
    private List<WaterSpring> springs = new();
    private float resistance = 40f;

    public void Init(SpriteShapeController ssc) 
    { 

        var index = transform.GetSiblingIndex();
        waveIndex = index+1;
        _spriteShapeController = ssc;

        Velocity = 0;
        Height = transform.localPosition.y;
        _targetHeight = transform.localPosition.y;
    }
    
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
    
    public void WavePointUpdate() 
    { 
        if (_spriteShapeController != null) 
        {
            Spline waterSpline = _spriteShapeController.spline;
            Vector3 wavePosition = waterSpline.GetPosition(waveIndex);
            waterSpline.SetPosition(waveIndex, new Vector3(wavePosition.x, transform.localPosition.y, wavePosition.z));
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag.Equals("Object"))
        {
            //Is this just getting the rigidbody?
            //FallingObject fallingObject = other.gameObject.GetComponent<FallingObject>();
            //Rigidbody2D rb = fallingObject.rigidbody2D;
            //var speed = rb.velocity;
            //Velocity += speed.y/resistance;

        }
    }
}
