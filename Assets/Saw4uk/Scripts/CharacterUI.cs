using System;
using TMPro;
using UnityEngine;

namespace Saw4uk.Scripts
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private AstronautShooting astronautShooting;
        [SerializeField] private TMP_Text ammoAmount;

        private void Awake()
        {
            astronautShooting.ammoChanged.AddListener(OnAmmoChanged);
            OnAmmoChanged(astronautShooting.CurrentAmmo);
        }

        private void OnAmmoChanged(int arg0)
        {
            ammoAmount.text = arg0.ToString();
        }
    }
}