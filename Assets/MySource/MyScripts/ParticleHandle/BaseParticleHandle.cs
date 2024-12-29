using UnityEngine;

public abstract class BaseParticleHandle : BaseMonoBehaviour
{
    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected float playInterval = 1f;
    private float playTimer;

    protected virtual void FixedUpdate()
    {
        this.playTimer += Time.fixedDeltaTime;
        if (this.playTimer >= this.playInterval)
        {
            if (!this.CanPlayParticle()) return;
            this.particle.Play();

            playTimer = 0;
        }
    }

    protected abstract bool CanPlayParticle();
}