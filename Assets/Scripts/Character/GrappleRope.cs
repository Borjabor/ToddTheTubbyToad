using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    [Header("General refrences:")]
    [SerializeField] private GrapplingGun _grapplingGun;
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int _precision = 20;
    [Range(0, 100)][SerializeField] private float _straightenLineSpeed = 4;

    [Header("Animation:")]
    public AnimationCurve ropeAnimationCurve;
    [SerializeField] [Range(0.01f, 4)] private float WaveSize = 20;
    float waveSize;

    [Header("Rope Speed:")]
    public AnimationCurve ropeLaunchSpeedCurve;
    [SerializeField] [Range(1, 50)] private float ropeLaunchSpeedMultiplayer = 4;

    float moveTime = 0;

    [SerializeField]public bool isGrappling = false;
    
    bool drawLine = true;
    bool straightLine = true;

    public Animator animator;
    

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.positionCount = _precision;
        waveSize = WaveSize;
    }

    private void OnEnable()
    {
        animator.SetTrigger("Hook");
        moveTime = 0;
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = _precision;
        waveSize = WaveSize;
        straightLine = false;
        LinePointToFirePoint();
    }

    private void OnDisable()
    {
        animator.SetTrigger("HookOff");
        _lineRenderer.enabled = false;
        isGrappling = false;
    }

    void LinePointToFirePoint()
    {
        for (int i = 0; i < _precision; i++)
        {
            _lineRenderer.SetPosition(i, _grapplingGun.transform.position);
        }
    }

    void Update()
    {
        moveTime += Time.deltaTime;

        if (drawLine)
        {
            DrawRope();
        }
    }

    void DrawRope()
    {
        if (!straightLine) 
        {
            if (_lineRenderer.GetPosition(_precision - 1).x != _grapplingGun.GrapplePoint.x)
            {
                DrawRopeWaves();
            }
            else 
            {
                straightLine = true;
            }
        }
        else 
        {
            if (!isGrappling) 
            {
                _grapplingGun.Grapple();
                isGrappling = true;
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * _straightenLineSpeed;
                DrawRopeWaves();
            }
            else 
            {
                waveSize = 0;
                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves() 
    {
        for (int i = 0; i < _precision; i++)
        {
            float delta = (float)i / ((float)_precision - 1f);
            Vector2 offset = Vector2.Perpendicular(_grapplingGun.DistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(_grapplingGun.transform.position, _grapplingGun.GrapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(_grapplingGun.transform.position, targetPosition, ropeLaunchSpeedCurve.Evaluate(moveTime) * ropeLaunchSpeedMultiplayer);

            _lineRenderer.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves() 
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _grapplingGun.GrapplePoint);
        _lineRenderer.SetPosition(1, _grapplingGun.transform.position);
    }

}
