using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class PlayerInputWalkingState : State
    {
        private BaseCharacter character;
        public float Speed { get; set; }
        public Vector2 MoveDirection { get; private set; }

        public PlayerInputWalkingState(BaseCharacter character, float speed) 
        {
            Speed = speed;
            this.character = character;
        }

        public override void OnPhysics()
        {
            character.Move(MoveDirection, Speed);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }

    }

    public abstract class BaseCharacter : MonoBehaviour
    {
        public abstract void Move(Vector2 direction, float speed);
    }
}
