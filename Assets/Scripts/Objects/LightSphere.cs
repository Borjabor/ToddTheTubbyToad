using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSphere : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var light = other.GetComponent<Light2D>();
        var player = other.gameObject.GetComponent<CharacterController>();
        if (light)
        {
            light.enabled = true;
        }

        if (player)
        {
            player.IsSafe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var light = other.GetComponent<Light2D>();
        var player = other.gameObject.GetComponent<CharacterController>();
        if (light)
        {
            light.enabled = false;
        }
        
        if (player)
        {
            player.IsSafe = false;
        }
    }
}
