using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public abstract class MonsterAction : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField] private int healthCost;
        [SerializeField] private Health health;
        [field: SerializeField] public float LastTimeUsed { get; private set; }        
        
        public float Cooldown => cooldown;
        
        public void OnButtonPressed(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.performed)
                return;
            if (!CanUse())
                return;
            Use();
        }

        public virtual bool CanUse()
        {
            return LastTimeUsed + cooldown < Time.time && 
                health.CurrentHealth - healthCost > 0;
        }

        protected void Use()
        {
            LastTimeUsed = Time.time;
            health.CurrentHealth -= healthCost;
            OnUse();
        }

        protected abstract void OnUse();
    }
}
