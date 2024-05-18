using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class AstronautCanSprintState : State
    {
        private PlayerInputWalkingState walkingState;
        private float normalSpeed;
        private float sprintSpeed;
        private float staminaGainPerSecond;
        private float staminaConsumptionPerSecond;
        private AstronautMovement astronaut;
        private Rigidbody2D rigidBody;

        public float Speed => walkingState.Speed;
        public Vector2 Direction => walkingState.MoveDirection;

        public bool IsSprinting { get; private set; }
        public AstronautCanSprintState(
            float normalSpeed, 
            float sprintSpeed,
            float staminaGainPerSecond,
            float staminaConsumptionPerSecond,
            AstronautMovement astronaut,
            Rigidbody2D rigidBody)
        {
            this.normalSpeed = normalSpeed;
            this.sprintSpeed = sprintSpeed;
            this.astronaut = astronaut;
            this.staminaGainPerSecond = staminaGainPerSecond;
            walkingState = new PlayerInputWalkingState(rigidBody, normalSpeed);
            this.staminaConsumptionPerSecond = staminaConsumptionPerSecond;
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
            }
            walkingState.OnLogic();
        }

        public override void OnPhysics()
        {
            if(IsSprinting && walkingState.MoveDirection != Vector2.zero)
            {
                astronaut.CurrentStamina -= staminaConsumptionPerSecond * Time.fixedDeltaTime;
            }
            else
            {
                astronaut.CurrentStamina += staminaGainPerSecond * Time.fixedDeltaTime;
            }
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
