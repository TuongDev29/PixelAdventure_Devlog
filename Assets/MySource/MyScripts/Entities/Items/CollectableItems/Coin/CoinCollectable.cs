using System;
using UnityEngine;

public class CoinCollectable : Collectable
{
    protected override void OnCollected(Collider2D collector)
    {
        InventoryManager.Instance.AddItemToInventory(this.collectableItemInfo);

        SpawnerManager.Instance.DespawnToPool(gameObject);
    }
}
