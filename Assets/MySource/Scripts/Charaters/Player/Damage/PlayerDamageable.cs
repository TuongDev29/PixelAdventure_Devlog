using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevLog;

namespace DevLog
{
    public class PlayerDamageable : Damageable
    {
        [Header("PlayerDamageable")]
        [SerializeField] protected float knockBackForce = 4f;
        [SerializeField] private float deadBounceForce = 2;
        [SerializeField] private PlayerController playerCtrl;

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.AutoLoadComponent<PlayerController>(ref this.playerCtrl, transform.GetComponent<PlayerController>());
        }

        protected override void ResetValue()
        {
            base.ResetValue();

            this.maxHealth = playerCtrl.PlayerDataSO.maxHealth;
        }

        protected override void OnReceiverDamage()
        {
            playerCtrl.PlayerState.ChangeState(EPlayerState.Hurt);
        }

        protected override void OnDamageReceivedFromSenderPosition(Vector2 senderPosition)
        {
            base.OnDamageReceivedFromSenderPosition(senderPosition);
            Vector2 direction = ((Vector2)transform.position - senderPosition).normalized;
            playerCtrl.rb.velocity = direction * knockBackForce;
        }

        protected override void OnDead()
        {
            //Add force on dead
            playerCtrl.Collider2D.isTrigger = true;
            playerCtrl.rb.AddForce(Vector2.up * deadBounceForce, ForceMode2D.Impulse);

            //Add torque on dead
            playerCtrl.rb.freezeRotation = false;
            playerCtrl.rb.AddTorque(2f, ForceMode2D.Impulse);

            playerCtrl.PlayerState.ChangeState(EPlayerState.Dead);
        }

        protected override void OnHealing()
        {
        }
    }

}