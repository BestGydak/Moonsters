using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Moonsters
{
    public class AstronautMovement : BaseCharacter
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private float normalSpeed;
        [Header("Sprint Settings")]
        [SerializeField] private float tiredSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float maxStamina;
        [SerializeField] private float staminaGainPerSeconds;
        [SerializeField] private float staminaConsumptionPerSeconds;
        [SerializeField] private float staminaToRest;
        [SerializeField] private float currentStamina;

        private StateMachine stateMachine;
        private AstronautCanSprintState canSprintState;
        private AstronautTiredState tiredState;
        
        public float CurrentStamina
        {
            get => currentStamina;
            set
            {
                var prevValue = currentStamina;
                currentStamina = Mathf.Clamp(value, 0, maxStamina);
                StaminaChanged?.Invoke(this, prevValue, currentStamina);
            }
        }

        public UnityEvent<AstronautMovement, float, float> StaminaChanged;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            CurrentStamina = maxStamina;
            stateMachine.SetState(canSprintState);
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
            canSprintState = new(normalSpeed, sprintSpeed, staminaGainPerSeconds, staminaConsumptionPerSeconds, this);
            tiredState = new(tiredSpeed, staminaGainPerSeconds, this);
            stateMachine.AddTransition(canSprintState, tiredState, () => CurrentStamina <= 0);
            stateMachine.AddTransition(tiredState, canSprintState, () => CurrentStamina >= staminaToRest);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            canSprintState.OnMove(context);
            tiredState.OnMove(context);
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            canSprintState.OnSprint(context);
        }

        public override void Move(Vector2 direction, float speed)
        {
            rigidBody.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
