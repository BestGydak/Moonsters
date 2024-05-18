using UnityEngine;

namespace Moonsters
{
    public class SpawnMiniMonsterAction : MonsterAction
    {
        [SerializeField] private MiniMonster miniMonsterPrefab;
        [SerializeField] private Health AstronautHealth;

        protected override void OnUse()
        {
            var monster = Instantiate(miniMonsterPrefab, transform.position, Quaternion.identity);
            monster.HealthTarget = AstronautHealth;
        }
    }
}
