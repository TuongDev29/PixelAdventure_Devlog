using UnityEngine;
using DevLog;

namespace DevLog
{
    public class EnemyDamageable : Damageable
    {
        [SerializeField] protected EnemyController enemyCtrl;
        [SerializeField] private float upwardForce = 4f;

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.AutoLoadComponent<EnemyController>(ref this.enemyCtrl, transform.GetComponent<EnemyController>());
        }

        protected override void ResetValue()
        {
            base.ResetValue();
            this.maxHealth = this.enemyCtrl.EnemyData.maxHealth;
        }

        protected override void OnDead()
        {
            enemyCtrl.Collider2D.isTrigger = true;
            enemyCtrl.rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
            enemyCtrl.rb.freezeRotation = false;
            enemyCtrl.rb.AddTorque(-12f, ForceMode2D.Impulse);
        }

        protected override void OnHealing()
        {
        }

        protected override void OnReceiverDamage()
        {
            enemyCtrl.anim.SetTrigger("hurt");
        }
    }

}