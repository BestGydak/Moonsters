using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class DashState : State
    {
        private Rigidbody2D character;
        private float speed;

        private Vector2 currentMoveDirection;
        private Vector2 dashMoveDirection;

        public Vector2 MoveDirection => currentMoveDirection;
        public float Speed => speed;

        public float LastTimeDashed { get; private set; }
        public DashState(Rigidbody2D character, float speed)
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
            character.MovePosition(character.position + dashMoveDirection * speed * Time.deltaTime);
        }

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            currentMoveDirection = callbackContext.ReadValue<Vector2>().normalized;
        }
    }
}
