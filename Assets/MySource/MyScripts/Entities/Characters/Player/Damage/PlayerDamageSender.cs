using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSender : DamageSender
{
    [Header("PlayerDamageSender")]
    [SerializeField] protected PlayerController playerCtrl;
    [SerializeField] protected float attackKnockbackForce = 4f;
    [SerializeField] private List<string> allowedTags;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.damage = playerCtrl.PlayerDataSO.damage;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAllowedTags();
        UntilityHelper.AutoFetchComponent<PlayerController>(ref this.playerCtrl, gameObject);
    }

    private void LoadAllowedTags()
    {
        if (this.allowedTags.Count > 0) return;
        this.allowedTags.Add("Enemy");
        this.allowedTags.Add("Item");

        Debug.LogWarning(transform.name + ": LoadAllowedTags", gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log($"Object Tag: {other.transform.tag} <===> IsTagAllowed{this.IsTagAllowed(other.transform.tag)}");
        if (this.IsTagAllowed(other.transform.tag))
        {
            IDamageable damageable = other.transform.GetComponent<IDamageable>();
            if (damageable == null) return;

            Vector2 direction = (other.transform.position - transform.position).normalized;
            float damageDotThreshold = GameSettingsManager.Instance.damageDotThreshold;
            // Debug.Log(Vector2.Dot(direction, Vector2.down));
            if (Vector2.Dot(direction, Vector2.down) <= damageDotThreshold) return;

            this.SendDamage(damageable);
            playerCtrl.rb.velocity = -direction * attackKnockbackForce;
        }
    }

    private bool IsTagAllowed(string tag)
    {
        foreach (string allowTag in this.allowedTags)
        {
            if (tag == allowTag) return true;
        }
        return false;
    }
}