using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class DashState : State
    {
        private UnityEvent<MonsterMovement> dashEvent;
        private MonsterMovement monsterMovement;
        private Rigidbody2D character;
        private float speed;

        private Vector2 currentMoveDirection;
        private Vector2 dashMoveDirection;

        public Vector2 MoveDirection => currentMoveDirection;
        public float Speed => speed;

        public float LastTimeDashed { get; private set; }
        public DashState(Rigidbody2D character, 
            float speed, 
            MonsterMovement monsterMovement,
            UnityEvent<MonsterMovement> dashEvent)
        {
            this.character = character;
            this.speed = speed;
            this.dashEvent = dashEvent;
            this.monsterMovement = monsterMovement;
        }

        public override void OnEnter()
        {
            dashEvent.Invoke(monsterMovement);
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
