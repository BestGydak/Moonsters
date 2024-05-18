using Pathfinding;
using SBA;
using UnityEngine;

namespace Moonsters
{
    public class MiniMonster : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private float speed;
        [SerializeField] private float distanceToAttack;
        [SerializeField] private float distanceToApproach;
        [Header("Attacking Settings")]
        [SerializeField] private Health healthTarget;
        [SerializeField] private int damage;
        [SerializeField] private float attackCooldown;
        [Header("Pathfinding Settings")]
        [SerializeField] private GameObject target;
        [SerializeField] private Seeker seeker;
        [SerializeField] private SteeringBehaviourAgent sba;
        [SerializeField] private float distanceToReachNode;
        [SerializeField] private float findPathDelayInSeconds;
        
        private StateMachine stateMachine;
        private PathfindingApproachingState approachingState;
        private AttackingState attackingState;

        public GameObject Target
        {
            get => target;
            set => target = value;
        }

        public Health HealthTarget
        {
            get => healthTarget;
            set => healthTarget = value;
        }

        private void Start()
        {
            InitializeStateMachine();
            stateMachine.SetState(attackingState);
        }

        private void Update()
        {
            stateMachine.OnLogic();
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
                target, 
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
            Destroy(gameObject);
        }

        private float DistanceToTarget() => Vector2.Distance(target.transform.position, transform.position);

    }
}
