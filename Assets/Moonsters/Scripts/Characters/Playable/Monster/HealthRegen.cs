using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public class HealthRegen : MonoBehaviour
    {
        [SerializeField] private Vector2Int randomHealAmount;
        [SerializeField] private float healIntervalInSeconds;
        [SerializeField] private Health health;
        [SerializeField] private int healDelayAfterHit;

        private bool isFrightened;
        private float lastTimeHit;

        private StateMachine stateMachine;
        private State frightenedState;
        private HealingState healingState;

        private void Start()
        {
            InitializeStateMachine();
            stateMachine.SetState(healingState);
            health.CurrentHealthChanged.AddListener(OnHPChanged);
        }

        private void FixedUpdate()
        {
            TryHeal();
        }

        private void TryHeal()
        {
            if (!isFrightened)
                return;
            if (lastTimeHit + healDelayAfterHit > Time.time)
                return;
            stateMachine.SetState(healingState);
            isFrightened = false;
        }    

        private void InitializeStateMachine()
        {
            stateMachine = new();
            frightenedState = new State();
            healingState = new HealingState(health, healIntervalInSeconds, randomHealAmount);
        }

        private void OnHPChanged(Health health, int previousValue, int newValue)
        {
            if (newValue >= previousValue)
                return;
            isFrightened = true;
            lastTimeHit = Time.time;
            stateMachine.SetState(frightenedState);
        }
    }
}
