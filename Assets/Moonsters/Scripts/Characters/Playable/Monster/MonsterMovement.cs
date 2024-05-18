using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class MonsterMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private float walkSpeed;

        [SerializeField] private Animator Animator;
        
        public Vector2 Direction => Vector2.down;
        public float Speed => 0.5f;
        
        [Header("Dash Settings")]
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDuration;
        [SerializeField] private float dashCooldown;

        private bool isDashed;

        private StateMachine stateMachine;
        private PlayerInputWalkingState walkingState;
        private DashState dashState;

        public float LastTimeDashed => dashState.LastTimeDashed;
        public float RemainingDashCooldown => LastTimeDashed + dashCooldown - Time.time;

        public Vector2 CurrentDirection
        {
            get
            {
                if (stateMachine.CurrentState == walkingState)
                {
                    return walkingState.MoveDirection;
                }
                if (stateMachine.CurrentState == dashState)
                {
                    return dashState.MoveDirection;
                }
                Debug.LogError("STUDID STATE!");
                return Vector2.zero;
            }
        }
        public float CurrentSpeed
        {
            get
            {
                if (CurrentDirection == Vector2.zero)
                    return 0;
                if (stateMachine.CurrentState == walkingState)
                {
                    return walkingState.Speed;
                }
                if (stateMachine.CurrentState == dashState)
                {
                    return dashState.Speed;
                }
                Debug.LogError("STUPID STATE!!!");
                return 0;
            }
        }

        private void Awake()
        {
            InitializeStateMachine();    
        }

        private void Start()
        {
            stateMachine.SetState(walkingState);
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

        public void InitializeStateMachine()
        {
            stateMachine = new();
            walkingState = new(rigidBody, walkSpeed);
            dashState = new(rigidBody, dashSpeed);
            stateMachine.AddTransition(walkingState, dashState, FromWalkingToDash);
            stateMachine.AddTransition(dashState, walkingState, FromDashToWalking);
        }

        private bool FromWalkingToDash()
        {
            return dashState.LastTimeDashed + dashCooldown < Time.time && isDashed;
        }

        private bool FromDashToWalking()
        {
            return dashState.LastTimeDashed + dashDuration < Time.time;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            walkingState.OnMove(context);
            dashState.OnMove(context);
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            isDashed = context.performed;
        }
    }
}
