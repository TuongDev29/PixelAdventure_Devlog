using System;
using UnityEngine;

public abstract class Collectable : BaseMonoBehaviour
{
    [SerializeField] protected Collider2D collider2d;
    [SerializeField] protected CollectableItemInfo collectableItemInfo;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTrigger2D();
        this.LoadDefaultItem();
    }

    private void LoadTrigger2D()
    {
        UntilityHelper.AutoFetchComponent<Collider2D>(ref this.collider2d, gameObject);
        this.collider2d.isTrigger = true;
    }

    private void LoadDefaultItem()
    {
        this.collectableItemInfo.collectableItemCode = UntilityHelper.TryParseEnum<ECollectableItemCode>(transform.name);
        this.collectableItemInfo.quantity = 1;

        string path = $"SO/Item/{transform.name}";
        this.collectableItemInfo.itemData = Resources.Load<ItemDataSO>(path);

        Debug.LogWarning(transform.name + ": LoadDefaultItem", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.OnCollected(other);
        }
    }

    protected abstract void OnCollected(Collider2D collector);
}
