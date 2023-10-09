using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpring : MonoBehaviour
{
    private float _velocity = 0;
    private float _force = 0;
    private float _height = 0;
    private float _targetHeight =0;
    
    public void WaveSpringUpdate(float _springStiffness)
    {
        _height = transform.position.y;
        //maximum extension
        var x = _height - _targetHeight;

        _force = -_springStiffness * x;
        _velocity += _force;
        var y = transform.position.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y+_velocity, transform.localPosition.z);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
