using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.Linq;

public class RopeSystem : MonoBehaviour
{
    [SerializeField] private GameObject _ropeHingeAnchor;
    [SerializeField] private DistanceJoint2D _ropeJoint;
    [SerializeField] private Transform _crosshair;
    [SerializeField] private SpriteRenderer _crosshairSprite;
    [SerializeField] private CharacterController _characterController;
    private bool _ropeAttached;
    private Vector2 _playerPosition;
    private Rigidbody2D _ropeHingeAnchorRb;
    private SpriteRenderer _ropeHingeAnchorSprite;
    private bool _distanceSet;
    
    [SerializeField] private LineRenderer _ropeRenderer;
    [SerializeField] private LayerMask _ropeLayerMask;
    [SerializeField] private float _ropeMaxCastDistance = 20f;
    private List<Vector2> _ropePositions = new List<Vector2>();

    void Awake()
    {
        _ropeJoint.enabled = false;
        _playerPosition = transform.position;
        _ropeHingeAnchorRb = _ropeHingeAnchor.GetComponent<Rigidbody2D>();
        //_ropeHingeAnchorSprite = _ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        _playerPosition = transform.position;
        HandleInput(aimDirection);
        UpdateRopePositions();
    }
    
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetMouseButton(0))
        {
            if (_ropeAttached) return;
            _ropeRenderer.enabled = true;

            var hit = Physics2D.Raycast(_playerPosition, aimDirection, _ropeMaxCastDistance, _ropeLayerMask);
        
            if (hit.collider != null)
            {
                _ropeAttached = true;
                if (!_ropePositions.Contains(hit.point))
                {
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    _ropePositions.Add(hit.point);
                    _ropeJoint.distance = Vector2.Distance(_playerPosition, hit.point);
                    _ropeJoint.enabled = true;
                    _ropeHingeAnchorSprite.enabled = true;
                }
            }
            else
            {
                _ropeRenderer.enabled = false;
                _ropeAttached = false;
                _ropeJoint.enabled = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            ResetRope();
        }
    }
    
    private void ResetRope()
    {
        _ropeJoint.enabled = false;
        _ropeAttached = false;
        _ropeRenderer.positionCount = 2;
        _ropeRenderer.SetPosition(0, transform.position);
        _ropeRenderer.SetPosition(1, transform.position);
        _ropePositions.Clear();
        _ropeHingeAnchorSprite.enabled = false;
    }
    
    private void UpdateRopePositions()
    {
        if (!_ropeAttached) return;

        _ropeRenderer.positionCount = _ropePositions.Count + 1;

        for (var i = _ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != _ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                _ropeRenderer.SetPosition(i, _ropePositions[i]);
                
                if (i == _ropePositions.Count - 1 || _ropePositions.Count == 1)
                {
                    var ropePosition = _ropePositions[_ropePositions.Count - 1];
                    if (_ropePositions.Count == 1)
                    {
                        _ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!_distanceSet)
                        {
                            _ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            _distanceSet = true;
                        }
                    }
                    else
                    {
                        _ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!_distanceSet)
                        {
                            _ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            _distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == _ropePositions.IndexOf(_ropePositions.Last()))
                {
                    var ropePosition = _ropePositions.Last();
                    _ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!_distanceSet)
                    {
                        _ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        _distanceSet = true;
                    }
                }
            }
            else
            {
                _ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }



}
