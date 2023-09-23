using UnityEngine;
using UnityEngine.Serialization;

public class Tongue : MonoBehaviour
{
    [Header("General refrences:")]
    [SerializeField] 
    private HookShot hookShot;
    [SerializeField] 
    private LineRenderer _lineRenderer;

    [Header("General Settings:")]
    [SerializeField] 
    private int _precision = 20;
    [Range(0, 100)][SerializeField] 
    private float _straightenLineSpeed = 4;

    [Header("Animation:")]
    [SerializeField] 
    private AnimationCurve ropeAnimationCurve;
    [SerializeField] [Range(0.01f, 4)] 
    private float _waveSize = 20;
    float _currentWaveSize;

    [Header("Rope Speed:")]
    [SerializeField] 
    private AnimationCurve _ropeLaunchSpeedCurve;
    [SerializeField] [Range(1, 50)] 
    private float _ropeLaunchSpeedMultiplayer = 4;

    private float _moveTime = 0;

    [SerializeField] 
    private bool _isGrappling = false;
    
    private bool _straightLine = true;

    [SerializeField] 
    private Animator _animator;
    

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.positionCount = _precision;
        _currentWaveSize = _waveSize;
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Hook");
        _moveTime = 0;
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = _precision;
        _currentWaveSize = _waveSize;
        _straightLine = false;
        LinePointToFirePoint();
    }

    private void OnDisable()
    {
        _animator.SetTrigger("HookOff");
        _lineRenderer.enabled = false;
        _isGrappling = false;
    }

    void LinePointToFirePoint()
    {
        for (int i = 0; i < _precision; i++)
        {
            _lineRenderer.SetPosition(i, hookShot.transform.position);
        }
    }

    void Update()
    {
        _moveTime += Time.deltaTime;

        DrawRope();
    }

    void DrawRope()
    {
        if (!_straightLine) 
        {
            if (_lineRenderer.GetPosition(_precision - 1).x != hookShot.GrapplePoint.x)
            {
                DrawRopeWaves();
            }
            else 
            {
                _straightLine = true;
            }
        }
        else 
        {
            if (!_isGrappling) 
            {
                hookShot.Grapple();
                _isGrappling = true;
            }
            if (_currentWaveSize > 0)
            {
                _currentWaveSize -= Time.deltaTime * _straightenLineSpeed;
                DrawRopeWaves();
            }
            else 
            {
                _currentWaveSize = 0;
                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves() 
    {
        for (int i = 0; i < _precision; i++)
        {
            float delta = (float)i / ((float)_precision - 1f);
            Vector2 offset = Vector2.Perpendicular(hookShot.DistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * _currentWaveSize;
            Vector2 targetPosition = Vector2.Lerp(hookShot.transform.position, hookShot.GrapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(hookShot.transform.position, targetPosition, _ropeLaunchSpeedCurve.Evaluate(_moveTime) * _ropeLaunchSpeedMultiplayer);

            _lineRenderer.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves() 
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, hookShot.GrapplePoint);
        _lineRenderer.SetPosition(1, hookShot.transform.position);
    }

}
