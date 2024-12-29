using System;

[Serializable]
public class CollectableItemInfo
{
    public ECollectableItemCode collectableItemCode;
    public ItemDataSO itemData;
    public int quantity;

    public CollectableItemInfo(ECollectableItemCode eItemCode, ItemDataSO itemDataSO, int quantity = 1)
    {
        this.collectableItemCode = eItemCode;
        this.itemData = itemDataSO;
        this.quantity = quantity;
    }
}
