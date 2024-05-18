using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class AstronautMovement : BaseCharacter
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private float normalSpeed;
        [Header("Sprint Settings")]
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float maxStamina;
        [SerializeField] private float staminaGainPerSeconds;
        [SerializeField] private float staminaConsumptionPerSeconds;

        private float currentStamina;
        public float CurrentStamina
        {
            get => currentStamina;
            set
            {
                var prevValue = currentStamina;
                currentStamina = Mathf.Max(0, value);
                StaminaChanged?.Invoke(this, prevValue, currentStamina);
            }
        }

        public UnityEvent<AstronautMovement, float, float> StaminaChanged;
        public override void Move(Vector2 direction, float speed)
        {
            rigidBody.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    public class AstronautCanSprintState : State
    {
        private PlayerInputWalkingState walkingState;
        private float normalSpeed;
        private float sprintSpeed;
        private float staminaGainPerSecond;
        private AstronautMovement astronaut;

        public bool IsSprinting { get; private set; }
        public AstronautCanSprintState(
            float normalSpeed, 
            float sprintSpeed,
            float staminaGainPerSecond,
            AstronautMovement astronaut)
        {
            this.normalSpeed = normalSpeed;
            this.sprintSpeed = sprintSpeed;
            this.astronaut = astronaut;
            this.staminaGainPerSecond = staminaGainPerSecond;
            walkingState = new PlayerInputWalkingState(astronaut, normalSpeed);
        }

        public override void OnEnter()
        {
            walkingState.OnEnter();
        }

        public override void OnExit()
        {
            walkingState.OnEnter();
        }

        public override void OnLogic()
        {
            if(IsSprinting)
            {
                walkingState.Speed = sprintSpeed;
            }
            else
            {
                walkingState.Speed = normalSpeed;
                astronaut.CurrentStamina += staminaGainPerSecond * Time.deltaTime;
            }
            walkingState.OnLogic();
        }

        public override void OnPhysics()
        {
            walkingState.OnPhysics();
        }

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            walkingState.OnMove(callbackContext);
        }

        public void OnSprint(InputAction.CallbackContext callbackContext)
        {
            IsSprinting = callbackContext.performed;
        }
    }
}
