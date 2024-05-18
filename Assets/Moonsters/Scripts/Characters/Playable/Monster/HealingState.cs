using System.Collections;
using UnityEngine;

namespace Moonsters
{
    public class HealingState : State
    {
        private Health health;
        private float healIntervalInSeconds;
        private Vector2Int randomHealAmount;

        private Coroutine coroutine;
        public HealingState(
            Health health, 
            float healIntervalInSeconds, 
            Vector2Int randomHealAmount)
        {
            this.health = health;
            this.healIntervalInSeconds = healIntervalInSeconds;
            this.randomHealAmount = randomHealAmount;
        }

        public override void OnEnter()
        {
            coroutine = health.StartCoroutine(HealCoroutine());
        }

        public override void OnExit()
        {
            health.StopCoroutine(coroutine);
        }

        private IEnumerator HealCoroutine()
        {
            while(true)
            {
                health.CurrentHealth += Random.Range(randomHealAmount.x, randomHealAmount.y + 1);
                yield return new WaitForSeconds(healIntervalInSeconds);
            }

        }
    }
}
