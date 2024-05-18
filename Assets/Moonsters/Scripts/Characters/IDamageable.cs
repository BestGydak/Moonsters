using UnityEngine;

namespace Moonsters
{
    public interface IDamageable
    {
        public void Damage(int damage);
        public void DamageNoGracePeriod(int damage);
        public void Damage(int damage, Vector2 attackDirection);
    }
}
