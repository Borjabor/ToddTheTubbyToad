using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
	private Rigidbody2D _rb;
	
	[SerializeField] 
	private LiveCount _liveCount;

	[Range(0, .3f)] [SerializeField]
	private float _movementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField]
	private bool _airControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField]
	private LayerMask _whatIsGround;							// A mask determining what is ground to the character
	[SerializeField]
	private Transform _groundCheck;							// A position marking where to check if the player is grounded.

	const float _groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool _grounded;            // Whether or not the player is grounded.
    private Vector2 _checkpoint;
    private bool _isRespawning = false;
    [SerializeField]
    private float _iFramesDuration;
    [SerializeField]
    private int _numberOfFlashes;

	//Audio
	private AudioSource _audioSource;
	[SerializeField] 
	private AudioClip _buffPickupAudio;
	[SerializeField] 
	private AudioClip _checkpointAudio;
	[SerializeField]
	private AudioClip _jumpAudio;
	[SerializeField]
	private AudioClip _deathAudio;


    [SerializeField]
    private float _moveSpeed = 60f;
	private float _horizontalMove = 0f;

	[Header("Knockback")]
    [SerializeField]
    private float _knockbackX = 25f;
	[SerializeField]
    private float _knockbackY = 5f;

    [Header("Jump")]
	[SerializeField]
	private float _jumpForce = 30f;

    [Header("Particles")]
	[SerializeField]
	private ParticleSystem _deathParticles;
	[SerializeField]
	private ParticleSystem _moveParticles;
	[SerializeField]
	private ParticleSystem _jumpParticles;

	[SerializeField] 
	private SpriteRenderer _bodyRenderer;
	[SerializeField]
	private GameObject _characterSprite;


	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
        _checkpoint = transform.position;
		_audioSource = GetComponent<AudioSource>();

    }

	void Update()
	{
		GetInputs();
		
		if(_horizontalMove != 0 && _rb.velocity.y == 0)
		{
			if (!_audioSource.isPlaying)
			{
				_audioSource.Play();
			}
		}
	}

	private void FixedUpdate()
	{
		if(!_isRespawning) Move(_horizontalMove * Time.fixedDeltaTime);
        if (_horizontalMove != 0 && _rb.velocity.y == 0)
        {
	        //_moveParticles.Play();
        }

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_grounded = true;
				_coyoteTimeCounter = _coyoteTime;
				if (!wasGrounded) OnLandEvent.Invoke();
			}
		}*/
	}


	private void Move(float move)
	{
		//Vector2 targetVelocity = new Vector2(move * 10f, _rb.velocity.y);
		// _rb.AddForce(Vector2.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, _movementSmoothing));
		_rb.AddForce(new Vector2(move * 10f, _rb.velocity.y));
	}

	private void GetInputs()
	{		
        _horizontalMove = Input.GetAxis("Horizontal") * _moveSpeed;
    }

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("Checkpoint"))
		{
			_audioSource.PlayOneShot(_checkpointAudio);
			_checkpoint = other.transform.position;
		}
		
		if (other.gameObject.CompareTag("Collectible"))
		{
			Destroy(other.gameObject);
			_audioSource.PlayOneShot(_buffPickupAudio);
			CollectiblesCounter.TotalPoints++;
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		foreach(ContactPoint2D hitPos in other.contacts)
		{
            if(hitPos.normal.y <= 0  && other.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(Respawn());
            }
			
			else if(hitPos.normal.y > 0  && other.gameObject.CompareTag("Enemy"))
            {
                _rb.velocity = Vector2.up * (_jumpForce/2);
            }
        }
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Spikes") && !_isRespawning)
		{
            //_rb.AddForce(new Vector2(-transform.localScale.x * _knockbackX, _knockbackY), ForceMode2D.Impulse);
            StartCoroutine(Respawn());
        }

		foreach(ContactPoint2D hitPos in other.contacts)
		{
            if(hitPos.normal.y <= 0  && other.gameObject.CompareTag("Enemy"))
            {
	            Physics2D.IgnoreLayerCollision(0, 6, true);
                StartCoroutine(Respawn());
            }
			else if(hitPos.normal.y > 0  && other.gameObject.CompareTag("Enemy"))
            {
                _rb.velocity = Vector2.up * (_jumpForce/2);
            }
        }
		
	}

	private IEnumerator Respawn()
	{
		_isRespawning = true;
		_liveCount.LoseLife();
		_rb.velocity = Vector2.zero;
		_audioSource.PlayOneShot(_deathAudio);
		_characterSprite.SetActive(false);
		_deathParticles.Play();
		yield return new WaitForSeconds(1.5f);
		if(_liveCount._remainingLives <= 0)
		{
			_liveCount._remainingLives = 5;
			transform.position = _checkpoint;
		}
		_characterSprite.SetActive(true);
		_isRespawning = false;
		
        
		StartCoroutine(Invulnerability());

    }

	private IEnumerator Invulnerability()
	{
		
		for (int i = 0; i < _numberOfFlashes; i++)
		{
			_bodyRenderer.color = new Color(0.8f, 0.2f, 0.2f, 0.5f);
			yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
			_bodyRenderer.color = Color.white;
			yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
		}
		Physics2D.IgnoreLayerCollision(0, 6, false);
	}

}
