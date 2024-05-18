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
                astronautShooting.AddHealth(healAmount);
                Destroy(gameObject);
            }
        }
    }
}