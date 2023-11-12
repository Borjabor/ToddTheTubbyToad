using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Tongue : MonoBehaviour
{
    [Header("General refrences:")]
    [SerializeField] 
    private CharacterController _player;
    [SerializeField] 
    private LineRenderer _lineRenderer;
    private CircleCollider2D _circleCollider;
    private Rigidbody2D _rb;

    [Header("General Settings:")]
    [SerializeField] 
    private int _precision = 20;
    [Range(0, 100)][SerializeField] 
    private float _straightenLineSpeed = 4;

    [Header("Animation:")]
    [SerializeField] 
    private Animator _animator;
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
    
    public bool _isGrappling {get; private set;}
    
    private bool _straightLine;
    

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
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
        _circleCollider.enabled = true;
        ChangeRigidbody(RigidbodyType2D.Dynamic);
        LinePointToFirePoint();
    }

    private void OnDisable()
    {
        _animator.SetTrigger("HookOff");
        _lineRenderer.enabled = false;
        _isGrappling = false;
        _rb.velocity = Vector3.zero;
        _circleCollider.enabled = false;
        ChangeRigidbody(RigidbodyType2D.Kinematic);
    }

    private void LinePointToFirePoint()
    {
        for (int i = 0; i < _precision; i++)
        {
            _lineRenderer.SetPosition(i, _player.transform.position);
        }
    }

    private void ChangeRigidbody(RigidbodyType2D bodyType)
    {
        _rb.bodyType = bodyType;
    }

    private void Update()
    {
        _moveTime += Time.deltaTime;
        DrawRope();
    }

    public void TongueDetach()
    {
        _isGrappling = false;
    }

    private void DrawRope()
    {
        if (!_straightLine) 
        {
            DrawRopeWaves();
        }
        else 
        {
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

    private void DrawRopeWaves() 
    {
        for (int i = 0; i < _precision; i++)
        {
            float delta = (float)i / ((float)_precision - 1f);
            Vector2 offset = Vector2.Perpendicular(_player.DistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * _currentWaveSize;
            Vector2 targetPosition = Vector2.Lerp(_player.transform.position, transform.position, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(_player.transform.position, targetPosition, _ropeLaunchSpeedCurve.Evaluate(_moveTime) * _ropeLaunchSpeedMultiplayer);
            _lineRenderer.SetPosition(i, currentPosition);
        }
    }

    private void DrawRopeNoWaves() 
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _player.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Collide {other.gameObject.name}");
        if (other.gameObject.layer == 3)
        {
            _isGrappling = true;
            _straightLine = true;
            _player.Grapple();
            _rb.velocity = Vector2.zero;
            ChangeRigidbody(RigidbodyType2D.Kinematic);
        }else if (other.gameObject.layer == 10)
        {
            _player.SetMovingObject(other.gameObject);
            _isGrappling = true;
            _straightLine = true;
            _player.Grapple();
            _rb.velocity = Vector2.zero;
            ChangeRigidbody(RigidbodyType2D.Kinematic);
        }else
        {
            _animator.SetTrigger("MissHook");
            Debug.Log($"Not Hit");
            _isGrappling = false;
            _player.FalseHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            _animator.SetTrigger("Gross");
            _isGrappling = false;
            _player.FalseHit();
            _circleCollider.enabled = false;
            return;
        }
        if (other.gameObject.layer == 3)
        {
            _isGrappling = true;
            _straightLine = true;
            _player.Grapple();
            _rb.velocity = Vector2.zero;
            ChangeRigidbody(RigidbodyType2D.Kinematic);
        }else if (other.gameObject.layer == 10)
        {
            _player.SetMovingObject(other.gameObject);
            _isGrappling = true;
            _straightLine = true;
            _player.Grapple();
            _rb.velocity = Vector2.zero;
            ChangeRigidbody(RigidbodyType2D.Kinematic);
        }else
        {
            _animator.SetTrigger("Gross");
            _isGrappling = false;
            _player.FalseHit();
        }
    }
}
