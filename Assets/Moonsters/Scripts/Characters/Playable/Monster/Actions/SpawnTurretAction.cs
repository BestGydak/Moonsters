using UnityEngine;

namespace Moonsters
{
    public class SpawnTurretAction : MonsterAction
    {
        [SerializeField] private Turret turretPrefab;
        [SerializeField] private Health AstronautHealth;

        protected override void OnUse()
        {
            var turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);
            turret.HealthTarget = AstronautHealth;
        }
    }
}
