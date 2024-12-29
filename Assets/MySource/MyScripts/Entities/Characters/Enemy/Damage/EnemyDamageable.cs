using UnityEngine;

public class EnemyDamageable : Damageable
{
    [SerializeField] protected EnemyController enemyCtrl;
    [SerializeField] private float deadBounceForce = 6f;
    [SerializeField] private float deadTorque = 24f;
    public float Health => this.health;
    public float MaxHealth => this.maxHealth;

    private Vector2 senderPosition;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<EnemyController>(ref this.enemyCtrl, gameObject);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.maxHealth = this.enemyCtrl.EnemyData.maxHealth;
    }

    protected override void OnDamageReceivedFromSenderPosition(Vector2 senderPosition)
    {
        base.OnDamageReceivedFromSenderPosition(senderPosition);
        this.senderPosition = senderPosition;
    }

    protected override void OnDead()
    {
        this.DeadEffect();
    }

    protected override void OnHealing()
    {
    }

    protected override void OnReceiverDamage()
    {
        enemyCtrl.anim.SetTrigger("hurt");
    }

    private void DeadEffect()
    {
        enemyCtrl.Collider2D.isTrigger = true;
        enemyCtrl.rb.freezeRotation = false;

        Vector2 bounceDirection = (senderPosition - (Vector2)transform.position).normalized;
        bounceDirection.x = Mathf.Sign(bounceDirection.x) * 0.2f;
        bounceDirection.y = Mathf.Sign(bounceDirection.y);

        enemyCtrl.rb.AddForce(bounceDirection * deadBounceForce, ForceMode2D.Impulse);
        enemyCtrl.rb.AddTorque(Mathf.Sign(bounceDirection.x) * this.deadTorque, ForceMode2D.Impulse);
    }
}
