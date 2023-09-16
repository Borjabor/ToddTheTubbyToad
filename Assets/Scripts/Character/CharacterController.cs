using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D _rb;

	[SerializeField] 
	private GameState _gameState;

	[Range(0, .3f)] [SerializeField]
	private float _movementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField]
	private bool _airControl;							// Whether or not a player can steer while jumping;
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
	
	private SpringJoint2D _springJoint;


    [SerializeField]
    private float _moveSpeed = 60f;
	private float _horizontalMove = 0f;

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


	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
        _checkpoint = transform.position;
		_audioSource = GetComponent<AudioSource>();
		_springJoint = GetComponent<SpringJoint2D>();

	}

	void Update()
	{
		GetInputs();
		float time = Time.timeScale;
		if (Input.GetKeyDown(KeyCode.Alpha1)) Time.timeScale = 1;
		if (Input.GetKeyDown(KeyCode.UpArrow) && Time.timeScale <= 1) Time.timeScale += 0.1f;
		if (Input.GetKeyDown(KeyCode.DownArrow) && Time.timeScale >= 0) Time.timeScale -= 0.1f;
		if(time != Time.timeScale) Debug.Log($"{time}");

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
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		_audioSource.PlayOneShot(_collideAudio);
		
	}

	private IEnumerator Respawn()
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
