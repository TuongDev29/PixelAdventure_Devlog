using UnityEngine;

namespace DevLog
{
    public interface IDamageable
    {
        public void Reborn();
        public void Receiver(int damage, Vector3 senderPosition);
        public void Healing(int hp);
    }
}