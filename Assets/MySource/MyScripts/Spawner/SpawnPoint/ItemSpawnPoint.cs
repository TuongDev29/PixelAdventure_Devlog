using System.Collections.Generic;
using UnityEngine;

public class CollectableItemSpawnPoint : BaseSpawnPoint<ECollectableItemCode>
{
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.spawnTag = ETagCode.Item;
    }
}