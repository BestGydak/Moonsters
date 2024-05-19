using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public class SpawnMiniMonsterAction : MonsterAction
    {
        [SerializeField] private MiniMonster miniMonsterPrefab;
        [SerializeField] private Health AstronautHealth;
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private AudioSource audioSource;

        protected override void OnUse()
        {
            var monster = Instantiate(miniMonsterPrefab, transform.position, Quaternion.identity);
            monster.HealthTarget = AstronautHealth;
            audioSource.PlayOneShot(audioClips.GetRandom());
        }
    }
}
