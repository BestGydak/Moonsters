using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public class MonsterSpawn : MonoBehaviour
    {
        [SerializeField] private Health monsterHealth;
        [SerializeField] private GameObject monster;
        [SerializeField] private int spawnHp;
        [Header("Cooldown Settings")]
        [SerializeField] private float baseCooldownInSeconds;
        [SerializeField] private float maxCooldownInSeconds;
        [SerializeField] private float cooldownIncrementInSeconds;

        [field: SerializeField] public float CurrentCooldownInSeconds { get; private set; }
        [field: SerializeField] public float UpCooldownInSeconds { get; private set; }   

        private bool isCounting;

        private void Start()
        {
            UpCooldownInSeconds = baseCooldownInSeconds;
            monsterHealth.Died.AddListener(OnDie);
        }

        private void Update()
        {
            if (!isCounting) return;
            CurrentCooldownInSeconds += Time.deltaTime;
            if(CurrentCooldownInSeconds >= UpCooldownInSeconds)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            isCounting = false;
            UpCooldownInSeconds += cooldownIncrementInSeconds;
            monster.SetActive(true);
            monsterHealth.IsAlive = true;
            monsterHealth.CurrentHealth = spawnHp;
            monster.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                monster.transform.position.z);
        }

        private void OnDie(Health health)
        {
            isCounting = true;
            CurrentCooldownInSeconds = 0f;
        }
    }
}