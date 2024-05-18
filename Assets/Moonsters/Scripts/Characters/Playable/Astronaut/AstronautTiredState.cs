using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class AstronautTiredState : State
    {
        private PlayerInputWalkingState walkingState;
        private float normalSpeed;
        private float staminaGainPerSecond;
        private AstronautMovement astronaut;
        private Rigidbody2D rigidBody;

        public Vector2 Direction => walkingState.MoveDirection;
        public float Speed => walkingState.Speed;

        public AstronautTiredState(
            float normalSpeed, 
            float staminaGainPerSecond, 
            AstronautMovement astronaut)
        {
            this.normalSpeed = normalSpeed;
            this.staminaGainPerSecond = staminaGainPerSecond;
            this.astronaut = astronaut;
            walkingState = new PlayerInputWalkingState(rigidBody, normalSpeed);
        }

        public override void OnEnter()
        {
            walkingState.OnEnter();
        }

        public override void OnExit()
        {
            walkingState.OnExit();
        }

        public override void OnLogic()
        {
            walkingState.OnLogic();
        }

        public override void OnPhysics()
        {
            astronaut.CurrentStamina += staminaGainPerSecond * Time.fixedDeltaTime;
            walkingState.OnPhysics();
        }

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            walkingState.OnMove(callbackContext);
        }
    }
}
