using UnityEngine;

public class BulletImpart : BaseMonoBehaviour
{
    [SerializeField] protected BulletController bulletCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<BulletController>(ref this.bulletCtrl, gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EffectSpawner.Instance.Spawn(EEffectCode.BreakBullet1, transform.position);
        this.bulletCtrl.BulletDespawn.Despawn();
    }
}