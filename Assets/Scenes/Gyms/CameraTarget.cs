using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraTarget : MonoBehaviour
{
    /* [SerializeField] Camera _cam;
    [SerializeField] Transform _player;
    [SerializeField] float _treshold;
  
    void Update()
    {
        Vector3 mousePos = _cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 targetPos = (_player.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -_treshold + _player.position.x, _treshold + _player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -_treshold + _player.position.y, _treshold + _player.position.y);

        this.transform.position = targetPos;


    }*/

    public void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }
}
