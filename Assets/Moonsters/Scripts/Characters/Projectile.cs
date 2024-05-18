using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Moonsters
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float liveTime = 5;
        [SerializeField] private Vector2Int randomDamage = new(2, 5);
        [SerializeField] private TeamTags.Type target;

        private new Rigidbody2D rigidbody;
        private bool isHit = false;
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
            if (isHit)
            {
                return;
            }

            if (!other.TryGetComponent<IDamageable>(out var damageComponent))
            {
                Destroy(gameObject);
                isHit = true;
                return;
            }
            if (!other.CompareTag(target.ToString()))
                return;
            var damage = Random.Range(randomDamage.x, randomDamage.y + 1);
            damageComponent.Damage(damage);
            Destroy(gameObject);
            isHit = true;
        }
    }
}