using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spawner<T> : BaseMonoBehaviour where T : Enum
{
    [SerializeField] protected Transform holder;
    protected Dictionary<Enum, ObjectPolling> objectPools;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadHolder();
        this.LoadObjectPools();
    }

    protected virtual void LoadObjectPools()
    {
        this.objectPools = new();

        Transform parentPrefabsTransform = UntilityHelper.EnsureChildTransform("Prefabs", gameObject);
        foreach (Transform prefab in parentPrefabsTransform)
        {
            string value = prefab.transform.name;
            objectPools.Add(UntilityHelper.TryParseEnum<T>(value), new ObjectPolling(prefab.gameObject, 1, this.holder));
        }
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = UntilityHelper.EnsureChildTransform("Holder", gameObject);

        Debug.LogWarning(transform.name + "LoadHolder", gameObject);
    }

    public GameObject Spawn(T ECode, Vector2 position)
    {
        ObjectPolling objectPolling = this.GetObjectPollingByType(ECode);

        GameObject obj = objectPolling.SpawnObjectFromPool(position);

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        string value = obj.transform.name;

        T type = UntilityHelper.TryParseEnum<T>(value);

        ObjectPolling objectPolling = this.GetObjectPollingByType(type);
        objectPolling.ReturnObjectToPool(obj);
    }

    protected ObjectPolling GetObjectPollingByType(T type)
    {
        if (!this.objectPools.ContainsKey(type))
        {
            Debug.LogWarning($"Despawn Cannot found Type:{type.ToString()} form objectPools");
            return null;
        }

        return this.objectPools[type];
    }
}