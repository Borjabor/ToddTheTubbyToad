using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HookShot : MonoBehaviour
{
    [Header("Scripts:")]
    [SerializeField] 
    private Tongue _tongue;
    [Header("Layer Settings:")]
    [SerializeField] 
    private bool _grappleToAll = false;
    [SerializeField] 
    private int _grappableLayerNumber = 3;

    [Header("Main Camera")]
    [SerializeField] 
    private Camera _camera;

    [Header("Distance:")]
    [SerializeField] 
    private bool _hasMaxDistance = true;
    [SerializeField] 
    private float _maxDistance = 4;

    [Header("Launching")]
    [Range(0, 5)] [SerializeField] 
    private float _launchSpeed = 5;
    [Header("This is Proportion of Distance Between Frog and Anchorpoint")]
    [Range(0, 1)][SerializeField]
    private float _distanceRatio = 0.5f;
    
    [Header("Mouse Cursor")] 
    [SerializeField]
    private Texture2D _canAttach;
    [SerializeField]
    private Texture2D _cannotAttach;
    
    [Header("Tongue Audio")]
    [SerializeField] 
    private AudioClip _tongueConnect;
    [SerializeField] 
    private AudioClip _tongueDisconnect;
    [SerializeField]
    private AudioClip _tongueThrow;
    [SerializeField] 
    private AudioSource _playerAudio;
    
    [FormerlySerializedAs("_springJoint2D")]
    [Header("Component Refrences:")]
    private SpringJoint2D _springJoint;
    public Vector2 GrapplePoint { get; private set; }
    public Vector2 DistanceVector{ get; private set; }
    private Vector2 _mouseFirePointDistanceVector;
    private Rigidbody2D _rb;
    [SerializeField]
    private float _tongueLengthChanger = 0.8f;

    private bool _hasPlayed = false;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _springJoint = GetComponent<SpringJoint2D>();
        _tongue.enabled = false;
        _springJoint.enabled = false;
        _rb.gravityScale = 1;
    }

    private void OnDisable()
    {
        _tongue.enabled = false;
        _springJoint.enabled = false;
        _rb.gravityScale = 1;
    }

    private void Update()
    { 
        _mouseFirePointDistanceVector = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        SetCursor();
        if (CharacterController._isRespawning) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_tongue.enabled)
            {
                if (!_hasPlayed)
                {
                    _playerAudio.PlayOneShot(_tongueThrow);
                    _hasPlayed = true;
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                _springJoint.distance -= (float)(_tongueLengthChanger * Time.deltaTime);
            }else if (Input.GetKey(KeyCode.S))
            {
                _springJoint.distance += (float)(_tongueLengthChanger * Time.deltaTime);
            }

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _tongue.enabled = false;
            _springJoint.enabled = false;
            _rb.gravityScale = 1;

            if (_hasPlayed)
            {
                _hasPlayed = false;
            }
        }
    }

    void SetGrapplePoint()
    {
        if (Physics2D.Raycast(transform.position, _mouseFirePointDistanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(transform.position, _mouseFirePointDistanceVector.normalized);
            if ((_hit.transform.gameObject.layer == _grappableLayerNumber || _grappleToAll) && ((Vector2.Distance(_hit.point, transform.position) <= _maxDistance) || !_hasMaxDistance))
            {
                GrapplePoint = _hit.point;
                DistanceVector = GrapplePoint - (Vector2)transform.position;
                _tongue.enabled = true;
            }
        }
    }
    
    void SetCursor()
    {
        if (Physics2D.Raycast(transform.position, _mouseFirePointDistanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(transform.position, _mouseFirePointDistanceVector.normalized);
            if ((_hit.transform.gameObject.layer == _grappableLayerNumber || _grappleToAll) && ((Vector2.Distance(_hit.point, transform.position) <= _maxDistance) || !_hasMaxDistance))
            {
                Cursor.SetCursor(_canAttach, Vector2.zero, CursorMode.ForceSoftware);
            }
            else
            {
                Cursor.SetCursor(_cannotAttach, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
    }
    
    

    public void Grapple()
    {
        _springJoint.connectedAnchor = GrapplePoint;
        _springJoint.distance = (GrapplePoint - (Vector2)transform.position).magnitude * _distanceRatio;
        _springJoint.frequency = _launchSpeed;
        _springJoint.enabled = true;
        _playerAudio.PlayOneShot(_tongueConnect);
    }

    private void OnDrawGizmos()
    {
        if (_hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _maxDistance);
        }
    }

}
