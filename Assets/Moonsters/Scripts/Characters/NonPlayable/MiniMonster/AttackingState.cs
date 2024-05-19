using System.Collections;
using UnityEngine;

namespace Moonsters
{
    public class AttackingState : State
    {
        private float attackCooldown;
        private int damage;
        private MonoBehaviour monster;
        private IDamageable target;

        private Coroutine attackCoroutine;

        public AttackingState(
            float attackCooldown, 
            int damage, 
            MonoBehaviour monster,
            IDamageable target)
        {
            this.attackCooldown = attackCooldown;
            this.damage = damage;
            this.monster = monster;
            this.target = target;
        }

        public override void OnEnter()
        {
            attackCoroutine = monster.StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(attackCooldown);
                target.Damage(damage);
            }
        }
    }
}
