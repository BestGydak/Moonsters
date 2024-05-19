using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;


    private void FixedUpdate()
    {
        if (!audioSource.isPlaying)
            Destroy(gameObject);
    }
}
