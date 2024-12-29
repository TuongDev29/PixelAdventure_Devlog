using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseSpawnPoint<T> : BaseMonoBehaviour where T : Enum
{
    public ETagCode spawnTag;
    public T spawnName;
    private GameObject obj;

    public virtual GameObject Spawn()
    {
        obj = SpawnerManager.Instance.SpawnFronPool(spawnTag.ToString(), spawnName.ToString(), transform.position, transform.rotation);
        return obj;
    }

    public virtual void Despawn()
    {
        if (obj == null) return;
        SpawnerManager.Instance.DespawnToPool(obj);
    }

}

public enum ETagCode
{
    Enemy,
    Trap,
    Item
}