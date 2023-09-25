using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class HookShot : MonoBehaviour
{
    [Header("Scripts:")]
    [SerializeField] 
    private Tongue _tongue;
    [Header("Layer Settings:")]
    [SerializeField] 
    private int _grappableLayerNumber = 3;

    [Header("Distance:")]
    [SerializeField] 
    private float _maxDistance = 4;

    [Header("Launching")]
    [Range(0, 5)] [SerializeField] 
    private float _launchSpeed = 5;
    [Header("This is Ration of Distance Between Frog and Anchorpoint")]
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
    
    public Vector2 GrapplePoint { get; private set; }
    public Vector2 DistanceVector{ get; private set; }
    private Vector2 _mouseFirePointDistanceVector;
    private SpringJoint2D _springJoint;
    private Rigidbody2D _rb;
    [SerializeField]
    private float _tongueLengthChanger = 0.8f;

    private bool _hasPlayed = false;
    private GameObject _movingObject;
    private GameObject _pullObject;
    private float _xOffset;
    private float _yOffset;
    
    private Camera _camera;
    private RaycastHit2D _hit;

    private void Awake()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        _springJoint = GetComponent<SpringJoint2D>();
        _tongue.enabled = false;
        _springJoint.enabled = false;
    }

    private void OnDisable()
    {
        _tongue.enabled = false;
        _springJoint.enabled = false;
    }

    private void Update()
    {
        SetCursor();
        if (CharacterController._isRespawning) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!_tongue.enabled) return;
            if (!_hasPlayed)
            {
                _playerAudio.PlayOneShot(_tongueThrow);
                _hasPlayed = true;
            }

            if (_movingObject && _springJoint.enabled)
            {
                var position = _movingObject.transform.position;
                _springJoint.connectedAnchor = new Vector2(position.x - _xOffset,position.y - _yOffset);
                GrapplePoint = _springJoint.connectedAnchor;
            }

            if (_pullObject && _springJoint.enabled)
            {
                var anchor = _pullObject.transform.position;
                _springJoint.connectedAnchor = anchor;
                GrapplePoint = _springJoint.connectedAnchor;
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
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _tongue.enabled = false;
            _springJoint.enabled = false;
            _movingObject = null;
            _pullObject = null;
            _springJoint.connectedBody = null;

            if (_hasPlayed)
            {
                _hasPlayed = false;
            }
        }
    }

    private void SetGrapplePoint()
    {
        if (!CanAttach()) return;
            
        DistanceVector = GrapplePoint - (Vector2)transform.position;
        _tongue.enabled = true;
        GrapplePoint = _hit.point;

        if (_hit.transform.gameObject.CompareTag("MovingObject"))
        {
            _movingObject = _hit.transform.gameObject;
            var position = _movingObject.transform.position;
            var connectedAnchor = _springJoint.connectedAnchor;
            _xOffset = position.x - connectedAnchor.x;
            _yOffset = position.y - connectedAnchor.y;
        }else if (_hit.transform.gameObject.CompareTag("PullObject"))
        {
            _springJoint.connectedBody = _hit.rigidbody;
            _pullObject = _hit.transform.gameObject;
        }
    }
    
    private void SetCursor()
    {
        if (CanAttach())
        {
            Cursor.SetCursor(_canAttach, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(_cannotAttach, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
    
    private bool CanAttach()
    {
        _mouseFirePointDistanceVector = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _hit = Physics2D.Raycast(transform.position, _mouseFirePointDistanceVector.normalized);
        return _hit.transform.gameObject.layer == _grappableLayerNumber && Vector2.Distance(_hit.point, transform.position) <= _maxDistance;
    }
    
    public void Grapple()
    {
        if (_pullObject) _rb.bodyType = RigidbodyType2D.Static;
        _springJoint.connectedAnchor = GrapplePoint;
        _springJoint.distance = (GrapplePoint - (Vector2)transform.position).magnitude * _distanceRatio;
        _springJoint.frequency = _launchSpeed;
        if (_movingObject)
        {
            var position = _movingObject.transform.position;
            var connectedAnchor = _springJoint.connectedAnchor;
            _xOffset = position.x - connectedAnchor.x;
            _yOffset = position.y - connectedAnchor.y;
        }
        _springJoint.enabled = true;
        _playerAudio.PlayOneShot(_tongueConnect);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _maxDistance);
    }

}
