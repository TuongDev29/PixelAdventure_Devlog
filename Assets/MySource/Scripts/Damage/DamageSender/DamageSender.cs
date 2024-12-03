using UnityEngine;
using DevLog;

namespace DevLog
{
    public class DamageSender : BaseMonoBehaviour, IDamageSender
    {
        [SerializeField] protected int damage = 1;

        public void SendDamage(IDamageable damageable)
        {
            damageable.Receiver(damage, transform.position);
        }
    }

}