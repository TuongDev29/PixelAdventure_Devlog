using UnityEngine;

namespace DevLog
{
    public class PlayerParticleController : MainMonoBehaviour
    {
        [SerializeField] private ParticleSystem movementParticle;
        [Range(1, 10f)][SerializeField] private int occurAffterVelocity;
        [Range(0, 0.2f)][SerializeField] private float dustFormationPeriod;
        [SerializeField] private Rigidbody2D rb;
        private float counter;

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.LoadRigidbody2D();
            this.LoadMovementPartical();
        }

        private void LoadRigidbody2D()
        {
            if (this.rb != null) return;

            this.rb = transform.parent.GetComponent<Rigidbody2D>();
            Debug.LogWarning(transform.name + "LoadRigidbody2D", gameObject);
        }

        private void LoadMovementPartical()
        {
            if (this.movementParticle != null) return;
            Transform particleTransform = this.EnsureChildTransform("MovementPartical");
            this.movementParticle = particleTransform.GetComponent<ParticleSystem>();
            Debug.LogWarning(transform.name + "LoadMovementPartical", gameObject);
        }

        protected virtual void FixedUpdate()
        {
            counter += Time.fixedDeltaTime;

            if (Mathf.Abs(this.rb.velocity.x) > occurAffterVelocity && Mathf.Abs(this.rb.velocity.y) == 0)
            {
                if (this.counter > dustFormationPeriod)
                {
                    this.movementParticle.Play();
                    this.counter = 0;
                }
            }
        }
    }

}