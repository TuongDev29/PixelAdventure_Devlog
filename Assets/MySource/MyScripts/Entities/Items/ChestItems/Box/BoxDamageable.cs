using System;
using UnityEngine;

public class BoxDamageable : Damageable
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected EEffectCode breakBoxEffectCode;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<Animator>(ref this.anim, gameObject);
    }

    protected override void OnDead()
    {
        Destroy(gameObject);
        EffectSpawner.Instance.Spawn(this.breakBoxEffectCode, transform.position);
    }

    protected override void OnHealing()
    {
    }

    protected override void OnReceiverDamage()
    {
        anim.SetTrigger("hit");
    }
}
