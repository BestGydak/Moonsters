using AkimboMayhem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonsters
{
    public abstract class MonsterAction : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField] private int healthCost;
        [SerializeField] private Health health;
        [field: SerializeField] public float LastTimeUsed { get; private set; }        
        
        public float Cooldown => cooldown;

        public virtual bool CanUse()
        {
            return LastTimeUsed + cooldown < Time.time && 
                health.CurrentHealth - healthCost > 0;
        }

        public void Use()
        {
            LastTimeUsed = Time.time;
            health.CurrentHealth -= healthCost;
            OnUse();
        }

        public abstract void OnUse();
    }
}
