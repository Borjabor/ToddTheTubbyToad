using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorOpen : MonoBehaviour
{
    public TextMeshProUGUI GoalReachable;
    [SerializeField] private GameObject _targetPos;
    private Vector2 _startPos;
    [SerializeField] private float _openSpeed = 4f;
    private bool _isOpening = false;
    private AudioSource _audioSource;

    private void Awake()
    {
        //GoalReachable.enabled = false;
        _startPos = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //EnableText();
        
        //Debug controls
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     _isOpening = true;
        // }
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     _isOpening = false;
        // }

        if (_isOpening)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, _openSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _targetPos.transform.position.y) _audioSource.Play();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _openSpeed * Time.deltaTime);
            if(!_audioSource.isPlaying && transform.position.y != _startPos.y) _audioSource.Play();
        }
        
        //StartCoroutine(FadeCoroutine());
    }
    IEnumerator FadeCoroutine()
    {
        float _waitTime = 2;
        while (_waitTime > 0)
        {
            GoalReachable.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.clear, Color.white, _waitTime));
            yield return null;
            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0.2)
            {
                GoalReachable.enabled = false;
            }
        }

    }

    public void OpenDoor()
    {
        _isOpening = true;
        //transform.position = Vector2.MoveTowards(transform.position, _targetPos.transform.position, _openSpeed * Time.deltaTime);
    }
    
    public void CloseDoor()
    {
        _isOpening = false;
        //transform.position = Vector2.MoveTowards(transform.position, _startPos, _openSpeed * Time.deltaTime);
    }

    public void EnableText()
    {
        GoalReachable.enabled = true;
    }

}
