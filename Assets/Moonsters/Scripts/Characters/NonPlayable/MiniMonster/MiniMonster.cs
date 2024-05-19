using Pathfinding;
using SBA;
using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public class MiniMonster : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private float speed;
        [SerializeField] private float distanceToAttack;
        [SerializeField] private float distanceToApproach;
        [SerializeField] private Animator Animator;

        [Header("Attacking Settings")]
        [SerializeField] private Health healthTarget;
        [SerializeField] private int damage;
        [SerializeField] private float attackCooldown;

        [Header("Pathfinding Settings")]
        [SerializeField] private Seeker seeker;
        [SerializeField] private SteeringBehaviourAgent sba;
        [SerializeField] private float distanceToReachNode;
        [SerializeField] private float findPathDelayInSeconds;

        [Header("SFX Settings")]
        [SerializeField] private List<SFX> sfxs;
        
        private StateMachine stateMachine;
        private PathfindingApproachingState approachingState;
        private AttackingState attackingState;

        public Health HealthTarget
        {
            get => healthTarget;
            set => healthTarget = value;
        }

        public Vector2 Direction
        {
            get
            {
                if(stateMachine.CurrentState == attackingState)
                {
                    return Vector2.zero;
                }
                return approachingState.Direction;
            }
        }

        public float Speed
        {
            get
            {
                if (stateMachine.CurrentState == attackingState)
                    return 0;
                return speed;
            }
        }

        private void Start()
        {
            InitializeStateMachine();
            stateMachine.SetState(attackingState);
        }

        private void Update()
        {
            stateMachine.OnLogic();
            Animator.SetFloat("Horizontal", Direction.y);
            Animator.SetFloat("Vertical", Direction.x);
            Animator.SetFloat("Speed", Speed);
        }

        private void FixedUpdate()
        {
            stateMachine.OnPhysics();
        }

        private void InitializeStateMachine()
        {
            stateMachine = new();
            approachingState = new(
                speed, 
                rigidBody, 
                healthTarget.gameObject, 
                seeker, 
                sba, 
                distanceToReachNode, 
                findPathDelayInSeconds);
            attackingState = new(attackCooldown, damage, this, healthTarget);
            stateMachine.AddTransition(approachingState, attackingState, () => DistanceToTarget() <= distanceToAttack);
            stateMachine.AddTransition(attackingState, approachingState, () => DistanceToTarget() >= distanceToApproach);
        }

        public void Die()
        {
            var sfx = sfxs.GetRandom();
            Instantiate(sfx);
            Destroy(gameObject);
        }

        private float DistanceToTarget() => Vector2.Distance(healthTarget.gameObject.transform.position, transform.position);

    }
}
