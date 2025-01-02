using UnityEngine;

public class BulletDamageSender : DamageSender
{
    [SerializeField] protected BulletController bulletCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<BulletController>(ref this.bulletCtrl, gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            this.SendDamage(damageable);
        }
    }
}