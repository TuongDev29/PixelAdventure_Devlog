using UnityEngine;

public class DustParticleHandler : BaseParticleHandle
{
    [Range(1, 10f)][SerializeField] private int occurAffterVelocity;
    [SerializeField] private Rigidbody2D rb;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchParentComponent<Rigidbody2D>(ref this.rb, gameObject);
        this.LoadMovementPartical();
    }

    private void LoadMovementPartical()
    {
        if (this.particle != null) return;
        Transform particleTransform = UntilityHelper.EnsureChildTransform("MovementPartical", gameObject);
        this.particle = particleTransform.GetComponent<ParticleSystem>();
        Debug.LogWarning(transform.name + ": LoadMovementPartical", gameObject);
    }

    protected override bool CanPlayParticle()
    {
        return Mathf.Abs(this.rb.velocity.x) > occurAffterVelocity &&
             Mathf.Abs(this.rb.velocity.y) == 0;
    }
}