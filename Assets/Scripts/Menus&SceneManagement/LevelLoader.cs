using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator _transition;
    private float _transitionTime = 1f;
    
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     ReloadCurrentLevel();
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReloadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadSpecificLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}
