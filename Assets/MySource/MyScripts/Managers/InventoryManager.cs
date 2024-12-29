using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] protected List<CollectableItemInfo> inventory;

    public void AddItemToInventory(CollectableItemInfo item)
    {
        if (item.itemData.itemType == EItemType.Consumable) return;

        foreach (var _ in this.inventory)
        {
            if (_.collectableItemCode != item.collectableItemCode) continue;

            _.quantity += item.quantity;
            _.quantity = Mathf.Min(_.quantity, _.itemData.maxSlot);
            return;
        }

        this.inventory.Add(new CollectableItemInfo(item.collectableItemCode, item.itemData, item.quantity));
    }
}
