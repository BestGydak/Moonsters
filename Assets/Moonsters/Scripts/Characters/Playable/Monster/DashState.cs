using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class DashState : State
    {
        private BaseCharacter character;
        private float speed;

        private Vector2 currentMoveDirection;
        private Vector2 dashMoveDirection;

        public float LastTimeDashed { get; private set; }
        public DashState(BaseCharacter character, float speed)
        {
            this.character = character;
            this.speed = speed;
        }

        public override void OnEnter()
        {
            LastTimeDashed = Time.time;
            dashMoveDirection = currentMoveDirection;
        }

        public override void OnPhysics()
        {
            character.Move(dashMoveDirection, speed);
        }

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            currentMoveDirection = callbackContext.ReadValue<Vector2>();
        }
    }
}
