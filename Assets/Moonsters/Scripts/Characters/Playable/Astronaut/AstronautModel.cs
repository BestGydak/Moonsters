using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class AstronautModel : MonoBehaviour
    {
        private Generator currentGenerator;
        
        public void SetGenerator(Generator generator)
        {
            currentGenerator = generator;
        }
        
        public void OnAction(InputAction.CallbackContext context)
        {
            if(context.performed && currentGenerator != null)
                currentGenerator.StartAction();
        }
    }
}