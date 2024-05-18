using System.Linq;
using UnityEngine;

namespace Moonsters
{
    public class AttackAction : MonsterAction
    {
        [Header("Attack Settings")]
        [SerializeField] private int damage;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask hitBoxMask;

        protected override void OnUse()
        {
            var playerCollider = Physics2D
                .OverlapCircleAll(transform.position, attackRadius, hitBoxMask)
                .Where(collider => collider.CompareTag(TeamTags.Astronaut))
                .FirstOrDefault();

            if (playerCollider == null)
                return;

            if (!playerCollider.TryGetComponent<IDamageable>(out var health))
                return;

            health.DamageNoGracePeriod(damage);
        }
    }
}
