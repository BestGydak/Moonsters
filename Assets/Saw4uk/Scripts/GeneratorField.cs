using System;
using System.Collections;
using System.Collections.Generic;
using Moonsters;
using UnityEngine;

public class GeneratorField : MonoBehaviour
{
    public event Action<AstronautModel> astronautChanged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var astronautMovement = other.GetComponent<AstronautModel>();
        if (astronautMovement)
        {
            astronautChanged?.Invoke(astronautMovement);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var astronautMovement = other.GetComponent<AstronautModel>();
        if (astronautMovement)
        {
            astronautChanged?.Invoke(null);
        }
    }
}
