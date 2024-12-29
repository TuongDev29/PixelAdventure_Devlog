using UnityEngine;

public class EnemyDamageSender : DamageSender
{
    [SerializeField] protected EnemyController enemyCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<EnemyController>(ref this.enemyCtrl, gameObject);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.damage = enemyCtrl.EnemyData.damage;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D collider2D = other.collider;

        if (collider2D.CompareTag("Player"))
        {
            IDamageable damageable = collider2D.GetComponent<IDamageable>();
            if (damageable == null) return;

            Vector2 direction = (transform.position - other.transform.position).normalized;

            float damageDotThreshold = GameSettingsManager.Instance.damageDotThreshold;
            if (Vector2.Dot(direction, Vector2.down) > damageDotThreshold) return;
            this.SendDamage(damageable);
        }
    }
}
