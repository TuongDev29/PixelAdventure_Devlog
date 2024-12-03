using UnityEngine;

namespace DevLog
{
    public interface IDamageSender
    {
        public void SendDamage(IDamageable damageable);
    }

}