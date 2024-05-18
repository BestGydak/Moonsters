using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Moonsters
{
    public class AmmoBox : MonoBehaviour
    {
        [SerializeField] private float ammoReplenishmentDelay;
        [SerializeField] private int ammoReplenishmentAmount;
        
        private AstronautShooting astronautShooting;
        private float remainingDelay;

        private void Update()
        {
            if (astronautShooting != null && remainingDelay <= 0 && !astronautShooting.IsFullAmmo)
            {
                astronautShooting.AddAmmo(ammoReplenishmentAmount);
                remainingDelay = ammoReplenishmentDelay;
            }
            else
                remainingDelay -= Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(astronautShooting != null)
                return;

            other.TryGetComponent<AstronautShooting>(out astronautShooting);
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if(astronautShooting == null)
                return;

            if (other.GetComponent<AstronautShooting>() != null)
                astronautShooting = null;
        }
    }
}