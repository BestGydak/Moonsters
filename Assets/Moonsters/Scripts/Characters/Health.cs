using UnityEngine.Events;
using UnityEngine;

namespace Moonsters
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float gracePeriodInSeconds;
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;

        public UnityEvent<Health, int, int> CurrentHealthChanged;
        public UnityEvent<Health, int, int> MaxHealthChanged;
        public UnityEvent<Health, int, int> Damaged;
        public UnityEvent<Health> Died;

        private float previousHitTime;
        private bool isAlive = true;

        public bool IsAlive => isAlive;
        public int CurrentHealth
        {
            set 
            {
                if (!isAlive)
                    return;
                var previousCurrentHealth = currentHealth;
                currentHealth = Mathf.Min(maxHealth, value); 
                CurrentHealthChanged?.Invoke(this, previousCurrentHealth, currentHealth);
                if(currentHealth <= 0)
                {
                   isAlive = false;
                   Died?.Invoke(this);
                }
                
            }
            get 
            { 
                return currentHealth; 
            }
        }

        public int MaxHealth
        {
            set
            {
                var previousMaxHealth = maxHealth;
                maxHealth = value;
                MaxHealthChanged?.Invoke(this, previousMaxHealth, maxHealth);
            }
            get
            {
                return maxHealth;
            }
        }

        public void Damage(int damage)
        {
            if (Time.time - previousHitTime < gracePeriodInSeconds)
                return;
            previousHitTime = Time.time;
            var prevHP = currentHealth;
            CurrentHealth -= damage;
            Damaged.Invoke(this, prevHP, currentHealth);
        }

        public void DamageNoGracePeriod(int damage)
        {
            var prevHP = currentHealth;
            CurrentHealth -= damage;
            Damaged.Invoke(this, prevHP, currentHealth);
        }

        public void Damage(int damage, Vector2 attackDirection)
        {
            Damage(damage);
        }
    }
}
