using UnityEngine;
using DevLog;

namespace DevLog
{
    public class PlayerDamageSender : DamageSender
    {
        [Header("PlayerDamageSender")]
        [SerializeField] protected PlayerController playerCtrl;
        [SerializeField] protected float attackKnockbackForce = 4f;

        protected override void ResetValue()
        {
            base.ResetValue();
            this.damage = playerCtrl.PlayerDataSO.damage;
        }

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.AutoLoadComponent<PlayerController>(ref this.playerCtrl, transform.GetComponent<PlayerController>());
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                IDamageable damageable = other.transform.GetComponent<IDamageable>();
                if (damageable == null) return;

                Vector2 direction = (other.transform.position - transform.position).normalized;
                float damageDotThreshold = GameConfigurationsManager.Instance.damageDotThreshold;
                if (Vector2.Dot(direction, Vector2.down) <= damageDotThreshold) return;

                this.SendDamage(damageable);
                playerCtrl.rb.velocity = -direction * attackKnockbackForce;
            }
        }
    }
}
