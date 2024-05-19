using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public class SpawnTurretAction : MonsterAction
    {
        [SerializeField] private Turret turretPrefab;
        [SerializeField] private Health AstronautHealth;
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private AudioSource audioSource;

        protected override void OnUse()
        {
            var turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);
            turret.HealthTarget = AstronautHealth;
            audioSource.PlayOneShot(audioClips.GetRandom());
        }
    }
}
