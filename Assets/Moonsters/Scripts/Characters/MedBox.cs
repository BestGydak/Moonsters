using UnityEngine;

namespace Moonsters
{
    public class MedBox : MonoBehaviour
    {
        [SerializeField] private int healAmount;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<AstronautShooting>(out var astronautShooting))
            {
                if (astronautShooting.Health.MaxHealth - astronautShooting.Health.CurrentHealth < healAmount)
                    return;
                astronautShooting.AddHealth(healAmount);
                Destroy(gameObject);
            }
        }
    }
}