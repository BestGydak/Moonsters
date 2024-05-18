using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class AstronautMovement : MonoBehaviour
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

        public Vector2 CurrentDirection
        {
            get
            {
                if (stateMachine.CurrentState == canSprintState)
                {
                    return canSprintState.Direction;
                }
                if(stateMachine.CurrentState == tiredState)
                {
                    return tiredState.Direction;
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
                if(stateMachine.CurrentState == canSprintState)
                {
                    return canSprintState.Speed;
                }
                if(stateMachine.CurrentState == tiredState)
                {
                    return tiredState.Speed;
                }
                Debug.LogError("STUPID STATE!!!");
                return 0;
            }
        }
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
            canSprintState = new(
                normalSpeed, 
                sprintSpeed, 
                staminaGainPerSeconds, 
                staminaConsumptionPerSeconds, 
                this, 
                rigidBody);
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
    }
}
