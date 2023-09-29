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
	private GameState _gameState;						// Whether or not a player can steer while jumping;
	/*
	[SerializeField]
	private LayerMask _whatIsGround;							// A mask determining what is ground to the character
	[SerializeField]
	private Transform _groundCheck;							// A position marking where to check if the player is grounded.

	const float _groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool _grounded;            // Whether or not the player is grounded.
	*/
    private Vector2 _checkpoint;
    public static bool _isRespawning;

	[Header("Audio")]
	private AudioSource _audioSource;
	[SerializeField]
	private AudioClip _collideAudio;
	[SerializeField]
	private AudioClip _deathAudio;
	[SerializeField] 
	private AudioClip _grabbing;

    [SerializeField]
    private float _moveSpeed = 60f;
	private float _horizontalMove;

    [Header("Particles")]
	[SerializeField]
	private ParticleSystem _deathParticles;
	[SerializeField]
	private ParticleSystem _moveParticles;
	[SerializeField]
	private ParticleSystem _jumpParticles;
	
	[SerializeField]
	private SpriteRenderer _characterSprite;
	[SerializeField] 
	private GameObject _arms;
	private Rigidbody2D _rb;
	
	private bool _isHolding;
	public bool IsSafe = true;

	[FormerlySerializedAs("_collider")]
	[SerializeField]
	private CircleCollider2D _triggerZone;
	


	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
        _checkpoint = transform.position;
		_audioSource = GetComponent<AudioSource>();
		_triggerZone.enabled = false;
	}

	void Update()
	{
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
			StartCoroutine(Respawn());
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
				_audioSource.PlayOneShot(_grabbing);
			}
			else
			{
				FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		_audioSource.PlayOneShot(_collideAudio);
	}

	public IEnumerator Respawn()
	{
		_isRespawning = true;
		_rb.velocity = Vector2.zero;
		_audioSource.PlayOneShot(_deathAudio);
		CameraShake.Instance.ShakeCamera(5f, 0.2f);
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

}
