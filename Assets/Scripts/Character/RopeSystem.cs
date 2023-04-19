using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RopeSystem : MonoBehaviour
{
    // 1
    [SerializeField] private GameObject _ropeHingeAnchor;
    [SerializeField] private DistanceJoint2D _ropeJoint;
    [SerializeField] private Transform _crosshair;
    [SerializeField] private SpriteRenderer _crosshairSprite;
    [SerializeField] private CharacterController2D _characterController;
    private bool ropeAttached;
    private Vector2 _playerPosition;
    private Rigidbody2D _ropeHingeAnchorRb;
    private SpriteRenderer _ropeHingeAnchorSprite;

    void Awake()
    {
        // 2
        _ropeJoint.enabled = false;
        _playerPosition = transform.position;
        _ropeHingeAnchorRb = _ropeHingeAnchor.GetComponent<Rigidbody2D>();
        //_ropeHingeAnchorSprite = _ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 3
        var worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        // 4
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        // 5
        _playerPosition = transform.position;

        // 6
        if (!ropeAttached)
        {
        }
        else
        {
        }
    }

}
