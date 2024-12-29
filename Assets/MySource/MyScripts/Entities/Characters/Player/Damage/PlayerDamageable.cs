using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    [Header("PlayerDamageable")]
    [SerializeField] protected PlayerController playerCtrl;
    [SerializeField] protected float knockBackForce = 4f;
    [SerializeField] private float deadBounceForce = 2;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<PlayerController>(ref this.playerCtrl, gameObject);
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

    public override void Reborn()
    {
        base.Reborn();
        playerCtrl.Collider2D.isTrigger = false;
        playerCtrl.rb.freezeRotation = true;

    }

    protected override void OnHealing()
    {
    }
}
