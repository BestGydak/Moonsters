using System;
using TMPro;
using UnityEngine;

namespace Saw4uk.Scripts
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private AstronautShooting astronautShooting;
        [SerializeField] private TMP_Text ammoAmount;
        private int maxAmmo;
        private void Awake()
        {
            astronautShooting.ammoChanged.AddListener(OnAmmoChanged);
            astronautShooting.maxAmmoChanged.AddListener(OnAmmoChanged);
            OnAmmoChanged(astronautShooting.CurrentAmmo);
            maxAmmo = astronautShooting.MaxAmmo;
        }

        private void OnAmmoChanged(int arg0)
        {
            ammoAmount.text = $"{astronautShooting.CurrentAmmo}/{astronautShooting.MaxAmmo}";
        }

    }
}