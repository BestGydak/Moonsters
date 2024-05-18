using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class PlayerInputWalkingState : State
    {
        private Rigidbody2D character;
        public float Speed { get; set; }
        public Vector2 MoveDirection { get; private set; }

        public PlayerInputWalkingState(Rigidbody2D character, float speed) 
        {
            Speed = speed;
            this.character = character;
        }

        public override void OnPhysics()
        {
            character.MovePosition(character.position + MoveDirection * Speed * Time.fixedDeltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>().normalized;
        }
    }
}
