using AkimboMayhem;
using Assets.Moonsters.Scripts.Characters;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public abstract class MonsterAction : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField] private int healthCost;
        [SerializeField] private Health health;
        [field: SerializeField] public float LastTimeUsed { get; private set; }        
        
        public float Cooldown => cooldown;
        
        public void OnButtonPressed(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.performed)
                return;
            if (!CanUse())
                return;
            Use();
        }

        public virtual bool CanUse()
        {
            return LastTimeUsed + cooldown < Time.time && 
                health.CurrentHealth - healthCost > 0;
        }

        public void Use()
        {
            LastTimeUsed = Time.time;
            health.CurrentHealth -= healthCost;
            OnUse();
        }

        public abstract void OnUse();
    }

    public class AttackAction : MonsterAction
    {
        [Header("Attack Settings")]
        [SerializeField] private int damage;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask hitBoxMask;
        public override void OnUse()
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
