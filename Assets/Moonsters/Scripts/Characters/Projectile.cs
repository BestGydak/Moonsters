using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float liveTime = 5;

    private new Rigidbody2D rigidbody;
    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector3 direction, float seed)
    {
        rigidbody.AddForce(direction * seed , ForceMode2D.Impulse);
        Destroy(gameObject, liveTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
