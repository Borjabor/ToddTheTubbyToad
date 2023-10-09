using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShapeController : MonoBehaviour
{
    [SerializeField]
    private float _springStiffness = 0.1f;
    [SerializeField]
    private List<WaterSpring> _springs = new();
    [SerializeField]
    private float _dampening = .03f;

    public float Spread = .006f;
    
    void FixedUpdate()
    {
        foreach (WaterSpring waterSpringComponent in _springs)
        {
            waterSpringComponent.WaveSpringUpdate(_springStiffness, _dampening);
        }

        UpdateSprings();
    }

    private void UpdateSprings()
    {
        int count = _springs.Count;
        float[] left_deltas = new float[count];
        float[] right_deltas = new float[count];

        for (int i = 0; i < count; i++)
        {
            if (i > 0)
            {
                left_deltas[i] = Spread * (_springs [i].Height - _springs[i-1].Height);
                _springs[i-1].Velocity += left_deltas[i];
            }

            if (i < _springs.Count - 1) 
            {
                right_deltas[i] = Spread * (_springs[i].Height - _springs[i+1].Height);
                _springs[i+1].Velocity += right_deltas[i];
            }
        }
    }

    private void Splash(int index, float speed) 
    { 
        if (index >= 0 && index < _springs.Count) 
        {
            _springs[index].Velocity += speed;
        }
    }
}
