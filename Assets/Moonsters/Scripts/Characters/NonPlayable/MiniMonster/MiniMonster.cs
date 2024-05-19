using Pathfinding;
using SBA;
using System.Collections;
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
        [SerializeField] private float spawnDelay;
        [Header("Attacking Settings")]
        [SerializeField] private Health healthTarget;
        [SerializeField] private int damage;
        [SerializeField] private float attackCooldown;

        [Header("Pathfinding Settings")]
        [SerializeField] private Seeker seeker;
        [SerializeField] private SteeringBehaviourAgent sba;
        [SerializeField] private float distanceToReachNode;
        [SerializeField] private float findPathDelayInSeconds;

        [SerializeField] private SpriteRenderer spriteRenderer;
        
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
            StartCoroutine(Coroutine());
            IEnumerator Coroutine()
            {
                yield return new WaitForSeconds(spawnDelay);
                stateMachine.SetState(attackingState);
            }
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
            attackingState.attacked += OnAttacked;
            stateMachine.AddTransition(approachingState, attackingState, () => DistanceToTarget() <= distanceToAttack);
            stateMachine.AddTransition(attackingState, approachingState, () => DistanceToTarget() >= distanceToApproach);
        }

        private void OnAttacked()
        {
            Animator.SetTrigger(healthTarget.transform.position.x < transform.position.x ? "AttackLeft" : "AttackRight");
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
