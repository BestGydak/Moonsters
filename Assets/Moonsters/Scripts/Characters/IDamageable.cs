using UnityEngine;

namespace AkimboMayhem
{
    public interface IDamageable
    {
        public void Damage(int damage);
        public void Damage(int damage, Vector2 attackDirection);
    }
}
