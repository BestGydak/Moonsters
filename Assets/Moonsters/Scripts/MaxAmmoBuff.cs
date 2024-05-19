using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public class MaxAmmoBuff : MonoBehaviour
    {
        [SerializeField] private int buff;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<AstronautShooting>(out var shooting))
            {
                shooting.MaxAmmo += buff;
                Destroy(gameObject);
            }
        }
    }
}
