using System;
using System.Collections;
using System.Collections.Generic;
using AkimboMayhem;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float liveTime = 5;
    [SerializeField] private Vector2Int randomDamage = new (2, 5);

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void LaunchProjectile(Vector3 direction, float speed)
    {
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
        Destroy(gameObject, liveTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.Damage(Random.Range(randomDamage.x, randomDamage.y + 1));

        Destroy(gameObject);
    }
}