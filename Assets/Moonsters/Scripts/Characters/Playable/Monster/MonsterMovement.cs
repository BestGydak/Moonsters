using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class MonsterMovement : BaseCharacter
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private float walkSpeed;
        [Header("Dash Settings")]
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDuration;
        [SerializeField] private float dashCooldown;

        private bool isDashed;

        private StateMachine stateMachine;
        private PlayerInputWalkingState walkingState;
        private DashState dashState;

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
        }

        private void FixedUpdate()
        {
            stateMachine.OnPhysics();
        }

        public void InitializeStateMachine()
        {
            stateMachine = new();
            walkingState = new(this, walkSpeed);
            dashState = new(this, dashSpeed);
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

        public override void Move(Vector2 direction, float speed)
        {
            rigidBody.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
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
