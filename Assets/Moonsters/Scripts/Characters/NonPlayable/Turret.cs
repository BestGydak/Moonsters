using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float minAttackDelay;
        [SerializeField] private float maxAttackDelay;
        [SerializeField] private Health healthTarget;
        [SerializeField] private float maxDistanceToAttack;
        [Header("Projectile Settings")]
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float projectileSpeed;
        
        public Health HealthTarget
        {
            get => healthTarget;
            set => healthTarget = value;
        }

        private void Start()
        {
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            while(true)
            {
                if(Vector2.Distance(healthTarget.transform.position, transform.position) > maxDistanceToAttack)
                {
                    yield return new WaitForFixedUpdate();
                    continue;
                }
                var direction = ((Vector2)healthTarget.transform.position - 
                    (Vector2)transform.position).normalized;
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.LaunchProjectile(direction, projectileSpeed);
                yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }    
    }
}
