using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
	[SerializeField]
	private GameState _gameState;
    private Vector2 _checkpoint;
    public static bool _isRespawning;
    
    [Header("Tongue Draw Script:")]
    [SerializeField] 
    private Tongue _tongue;
    [Header("Layer Settings:")]
    [SerializeField] 
    private int _grappableLayerNumber = 3;
    
    [Header("Attach Distance:")]
    [SerializeField] 
    private float _maxDistance = 4;
    
    [Header("Tongue Launch")]
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

	[Header("Audio")]
	[SerializeField]
	private AudioClip _collideAudio;
	[SerializeField]
	private AudioClip _deathAudio;
	[SerializeField] 
	private AudioClip _grabbing;
	private AudioSource _audioSource;
	
	[SerializeField] 
	private AudioClip _tongueConnect;
	[SerializeField] 
	private AudioClip _tongueDisconnect;
	[SerializeField]
	private AudioClip _tongueThrow;

    [Header("Particles")]
	[SerializeField]
	private ParticleSystem _deathParticles;
	[SerializeField]
	private ParticleSystem _moveParticles;
	[SerializeField]
	private ParticleSystem _jumpParticles;
	
	[Header("Other")]
	[SerializeField]
	private float _moveSpeed = 60f;
	private float _horizontalMove;
	[SerializeField]
	private SpriteRenderer _characterSprite;
	[SerializeField] 
	private GameObject _arms;
	private Rigidbody2D _rb;
	[SerializeField]
	private HookShot _hookShot;
	[SerializeField]
	private Collider2D _triggerZone;
	
	private bool _isHolding;
	public bool IsSafe = true;
	private Bubble _currentBubble;
	
	//Hookshot Data
	public Vector2 GrapplePoint { get; private set; }
	public Vector2 DistanceVector{ get; private set; }
	private Vector2 _mouseFirePointDistanceVector;
	private SpringJoint2D _springJoint;
	[SerializeField]
	private float _tongueLengthChanger = 0.8f;

	private bool _hasPlayed = false;
	private GameObject _movingObject;
	private GameObject _pullObject;
	private float _xOffset;
	private float _yOffset;
    
	private Camera _camera;
	private RaycastHit2D _hit;
	private bool _isStuck;


	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
        _checkpoint = transform.position;
		_audioSource = GetComponent<AudioSource>();
		_triggerZone.enabled = false;
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

	void Update()
	{
		if (_isRespawning) return;
		SetCursor();
		GetInputs();
		// float time = Time.timeScale;
		// if (Input.GetKeyDown(KeyCode.Alpha1)) Time.timeScale = 1;
		// if (Input.GetKeyDown(KeyCode.UpArrow) && Time.timeScale <= 1) Time.timeScale += 0.1f;
		// if (Input.GetKeyDown(KeyCode.DownArrow) && Time.timeScale >= 0) Time.timeScale -= 0.1f;
		// if (Input.GetKeyDown(KeyCode.F)) _rb.gravityScale = -_rb.gravityScale;
		// if(time != Time.timeScale) Debug.Log($"{time}");
		

	}

	private void FixedUpdate()
	{
		if(!_isRespawning) Move(_horizontalMove * Time.fixedDeltaTime);
        if (_horizontalMove != 0 && _rb.velocity.y == 0)
        {
	        //_moveParticles.Play();
        }
	}
	
	private void Move(float move)
	{
		if (_currentBubble)
		{
			_currentBubble.Move(move);
			return;
		}
		_rb.AddForce(new Vector2(move * 10f, 0f));
	}

	private void GetInputs()
	{		
        _horizontalMove = Input.GetAxis("Horizontal") * _moveSpeed;
        if (Input.GetKey(KeyCode.Space))
        {
	        _isHolding = true;
	        _triggerZone.enabled = true;
        }
        else
        {
	        _isHolding = false;
	        _triggerZone.enabled = false;
	        Tentacle.GrabbedObject = null;
	        Destroy(GetComponent<FixedJoint2D>());
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
	        SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
	        if (!_tongue.enabled) return;
	        if (!_hasPlayed)
	        {
		        _audioSource.PlayOneShot(_tongueThrow);
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
		        if (_springJoint.distance <= 2) _springJoint.distance = 2;
	        }else if (Input.GetKey(KeyCode.S))
	        {
		        _springJoint.distance += (float)(_tongueLengthChanger * Time.deltaTime);
		        if (_springJoint.distance >= _maxDistance) _springJoint.distance = _maxDistance;
	        }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
	        Detach();
        }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("Checkpoint"))
		{
			//_audioSource.PlayOneShot(_checkpointAudio);
			_checkpoint = other.transform.position;
		}
		
		if(other.gameObject.CompareTag("Hazard") && !_isRespawning)
		{
			Die();
		}
		
		if (other.gameObject.GetComponent<Bubble>())
		{
			_currentBubble = other.gameObject.GetComponent<Bubble>();
			StartCoroutine(GetInBubble());
		}
		
		if(GetComponent<FixedJoint2D>() != null) return;
		if (other.gameObject.CompareTag("Object"))
		{
			Rigidbody2D rb = other.transform.GetComponent<Rigidbody2D>();
			Tentacle.GrabbedObject = other.gameObject;
			if (rb != null)
			{
				FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
				fj.connectedBody = rb;
				fj.enableCollision = true;
				//other.gameObject.layer = 2;
				_audioSource.PlayOneShot(_grabbing);
			}
			else
			{
				FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		//if (other.gameObject.CompareTag("Object")) other.gameObject.layer = 3;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		_audioSource.PlayOneShot(_collideAudio);
		var sticky = other.gameObject.GetComponent<StickySurface>();
		_isStuck = true;
		if (sticky) _rb.bodyType = RigidbodyType2D.Static;
		
		if(other.gameObject.CompareTag("Hazard") && !_isRespawning)
		{
			Die();
		}
		
		if (other.gameObject.GetComponent<Bubble>())
		{
			_currentBubble = other.gameObject.GetComponent<Bubble>();
			StartCoroutine(GetInBubble());
		}
	}

	public void Die()
	{
		StartCoroutine(Respawn());
        CameraShake.Instance.ShakeCamera(5f, 0.2f);
    }

    private IEnumerator Respawn()
	{
		_isRespawning = true;
		IsSafe = true;
		_rb.velocity = Vector2.zero;
		Detach();
		_audioSource.PlayOneShot(_deathAudio);
		//CameraShake.Instance.ShakeCamera(5f, 0.2f);
		_deathParticles.Play();
		_characterSprite.enabled = false;
		_arms.SetActive(false);
		yield return new WaitForSeconds(1.5f);
		transform.position = _checkpoint;
		_deathParticles.Stop();
		_characterSprite.enabled = true;
		_arms.SetActive(true);
		_isRespawning = false;
	}

	private IEnumerator GetInBubble()
	{
		transform.SetParent(_currentBubble.transform);
		_rb.isKinematic = true;
		while (transform.position != _currentBubble.transform.position)
		{
			transform.position = Vector2.MoveTowards(transform.position, _currentBubble.transform.position, 0.01f);
			yield return null;
		}

		if (Vector2.Distance(transform.position, _currentBubble.transform.position) <= 0.1f)
			transform.position = _currentBubble.transform.position;
		Debug.Log($"in bubble");
	}
	
	//Hookshot methods
	private void SetGrapplePoint()
    {
        if (!CanAttach()) return;

        if (_currentBubble)
        {
	        transform.parent = null;
	        _currentBubble.Pop();
	        _rb.isKinematic = false;
        }
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
        }else if (_hit.transform.gameObject.GetComponent<DissolveObject>())
        {
	        if(_hit.rigidbody.bodyType == RigidbodyType2D.Static) return;
            _springJoint.connectedBody = _hit.rigidbody;
            _pullObject = _hit.transform.gameObject;
        }
    }
	
	public void Detach()
	{
		if (!_isStuck) _rb.bodyType = RigidbodyType2D.Dynamic;
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
    
    private bool CanAttach()
    {
        _mouseFirePointDistanceVector = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _hit = Physics2D.Raycast(transform.position, _mouseFirePointDistanceVector.normalized);
        return _hit.transform.gameObject.layer == _grappableLayerNumber && Vector2.Distance(_hit.point, transform.position) <= _maxDistance;
    }
    
    public void Grapple()
    {
        _rb.bodyType = _pullObject ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        if (_rb.bodyType == RigidbodyType2D.Dynamic) _isStuck = false;
        var fixedJoint = GetComponent<FixedJoint2D>();
        if (fixedJoint != null) fixedJoint.connectedBody.bodyType = RigidbodyType2D.Dynamic;
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
        _audioSource.PlayOneShot(_tongueConnect);
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
    
    private void OnDrawGizmos()
    {
	    Gizmos.color = Color.green;
	    Gizmos.DrawWireSphere(transform.position, _maxDistance);
    }

}
