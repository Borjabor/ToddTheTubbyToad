using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways]
public class WaterShapeController : MonoBehaviour
{
    private int _cornersCount = 2;
    [SerializeField]
    private SpriteShapeController _spriteShapeController;
   [SerializeField]
    private GameObject _wavePointPref;
    [SerializeField]
    private GameObject _wavePoints;
   
    [SerializeField]
    [Range (1,100)]
    private int _wavesCount = 6;
    
    [SerializeField]
    private List<WaterSpring> _springs = new();
    [SerializeField]
    private float _springStiffness = 0.1f;
    [SerializeField]
    private float _dampening = .03f;

    public float Spread = .006f;

    void OnValidate()
    {
        // Clean waterpoints 
        StartCoroutine(CreateWaves());
    }

    IEnumerator CreateWaves()
    {
        foreach (Transform child in _wavePoints.transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }
        yield return null;
        SetWaves();
        yield return null;
    }

    IEnumerator Destroy(GameObject go) 
    {
        yield return null;
        DestroyImmediate(go);
    }

    private void SetWaves()
    {
        Spline waterSpline = _spriteShapeController.spline;
        int waterPointsCount = waterSpline.GetPointCount();

        // Remove middle points for the waves
        // Keep only the corners
        // Removing 1 point at a time we can remove only the 1st point
        // This means every time we remove 1st point the 2nd point becomes first
        for (int i = _cornersCount; i < waterPointsCount - _cornersCount; i++) 
        {
            waterSpline.RemovePointAt(_cornersCount);
        }
        
        Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
        Vector3 waterTopRightCorner = waterSpline.GetPosition(2);
        float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;

        float spacingPerWave = waterWidth / (_wavesCount+1);
        // Set new points for the waves
        for (int i = _wavesCount; i > 0 ; i--) 
        {
            int index = _cornersCount;

            float xPosition = waterTopLeftCorner.x + (spacingPerWave*i);
            Vector3 wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
            waterSpline.InsertPointAt(index, wavePoint);
            waterSpline.SetHeight(index, 0.1f);
            waterSpline.SetCorner(index, false);
            waterSpline.SetTangentMode(index, ShapeTangentMode.Continuous);
        }
    }

    //For some reason this part is different in the tutorial
    private void CreateSprings(Spline waterSpline)
    {
        _springs = new();
        for (int i = 0; i <= _wavesCount+1; i++) 
        {
            int index = i + 1;

            Smoothen(waterSpline, index); 

            GameObject wavePoint = Instantiate(_wavePointPref, _wavePoints.transform, false);
            wavePoint.transform.localPosition = waterSpline.GetPosition(index);

            WaterSpring waterSpring = wavePoint.GetComponent<WaterSpring>();
            waterSpring.Init(_spriteShapeController);
            _springs.Add(waterSpring);

            // WaveSpring waveSpring = wavePoint.GetComponent<WaveSpring>();
            // waveSpring.Init(spriteShapeController);
        }
    }

    private void Smoothen(Spline waterSpline, int index)
    {
        Vector3 position = waterSpline.GetPosition(index);
        Vector3 positionPrev = position;
        Vector3 positionNext = position;
        if (index > 1) {
            positionPrev = waterSpline.GetPosition(index-1);
        }
        if (index - 1 <= _wavesCount) {
            positionNext = waterSpline.GetPosition(index+1);
        }

        Vector3 forward = gameObject.transform.forward;

        float scale = Mathf.Min((positionNext - position).magnitude, (positionPrev - position).magnitude) * 0.33f;

        Vector3 leftTangent = (positionPrev - position).normalized * scale;
        Vector3 rightTangent = (positionNext - position).normalized * scale;

        SplineUtility.CalculateTangents(position, positionPrev, positionNext, forward, scale, out rightTangent, out leftTangent);
        
        waterSpline.SetLeftTangent(index, leftTangent);
        waterSpline.SetRightTangent(index, rightTangent);
    }

    void FixedUpdate()
    {
        foreach (WaterSpring waterSpringComponent in _springs)
        {
            waterSpringComponent.WaveSpringUpdate(_springStiffness, _dampening);
            waterSpringComponent.WavePointUpdate();
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
