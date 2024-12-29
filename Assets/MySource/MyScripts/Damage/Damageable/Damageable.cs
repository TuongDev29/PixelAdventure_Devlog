using UnityEngine;

public abstract class Damageable : BaseMonoBehaviour, IDamageable
{
    [Header("Damageable")]
    [SerializeField] protected bool isDead;
    public bool IsDead => isDead;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;

    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    public virtual void Reborn()
    {
        this.health = maxHealth;
        this.isDead = false;
    }

    public void Healing(int hp)
    {
        if (this.health >= this.maxHealth) return;

        this.health += hp;
        this.OnHealing();
        this.CheckingHealth();
    }

    public void Receiver(int damage, Vector3 senderPosition)
    {
        this.OnDamageReceivedFromSenderPosition(senderPosition);
        this.Receiver(damage);
    }

    public void Receiver(int damage)
    {
        if (health <= 0) return;

        this.health -= damage;
        this.OnReceiverDamage();
        this.CheckingHealth();
    }

    protected void CheckingHealth()
    {
        if (this.isDead) return;

        if (this.health > this.maxHealth) this.health = this.maxHealth;
        if (this.health < 0) this.health = 0;

        if (this.health == 0)
        {
            this.isDead = true;
            this.OnDead();
        }
    }

    protected virtual void OnDamageReceivedFromSenderPosition(Vector2 senderPosition) { }

    protected abstract void OnHealing();
    protected abstract void OnReceiverDamage();
    protected abstract void OnDead();
}
