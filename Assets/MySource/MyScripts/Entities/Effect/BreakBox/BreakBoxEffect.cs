using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBoxEffect : BaseMonoBehaviour
{
    [SerializeField] protected List<Rigidbody2D> rbFragments;
    [SerializeField] protected float explosionForce = 4f;
    [SerializeField] protected float upwardBias = 2f;
    [SerializeField] protected float torqueRange;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadFragments();
    }

    protected virtual void OnEnable()
    {
        this.Break();
    }

    private void LoadFragments()
    {
        if (this.rbFragments.Count > 0) return;

        foreach (Transform child in transform)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                this.rbFragments.Add(rb);
            }
        }

        Debug.LogWarning(transform.name + ": RbLoadFragments", gameObject);
    }

    public void Break()
    {
        foreach (var rb in this.rbFragments)
        {
            Vector2 randDirection = Random.insideUnitCircle.normalized;
            randDirection.y += this.upwardBias;

            rb.AddForce(randDirection * this.explosionForce, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-torqueRange, torqueRange));

            CoroutineManager.Instance.StartManagedCoroutine(this.DelayDes());
        }
    }

    private void ResetFragments()
    {
        foreach (var rb in this.rbFragments)
        {
            rb.transform.localPosition = Vector2.zero;
            rb.gameObject.SetActive(true);
        }
    }

    public IEnumerator DelayDes()
    {
        int count = this.rbFragments.Count;
        float minTimer = 5.4f;

        while (count > 0)
        {
            if (count > 1)
            {
                float delay = Random.Range(1f, 1.4f);
                minTimer -= delay;
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return new WaitForSeconds(minTimer);
            }

            Rigidbody2D rb = this.rbFragments[count - 1];
            rb.gameObject.SetActive(false);

            count--;
        }

        this.ResetFragments();

        EffectSpawner.Instance.Despawn(gameObject);
    }
}
