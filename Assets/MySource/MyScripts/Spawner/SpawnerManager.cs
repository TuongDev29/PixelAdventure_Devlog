using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : BaseMonoSingleton<SpawnerManager>
{
    protected Dictionary<string, Dictionary<string, ObjectPolling>> objectPools;
    [SerializeField] List<string> tags = new();
    [SerializeField] List<string> names = new();
    [SerializeField] protected Transform holder;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadHolder();
        this.LoadObjectPools();
    }

    private void LoadObjectPools()
    {
        this.objectPools = new();

        Transform prefabsHol = UntilityHelper.EnsureChildTransform("Prefabs", gameObject);

        foreach (Transform prefab in prefabsHol)
        {
            string tag = prefab.tag;
            string name = prefab.name;

            if (tag == "Untagged") continue;
            if (!this.objectPools.ContainsKey(tag))
            {
                this.objectPools.Add(tag, new Dictionary<string, ObjectPolling>());
                this.tags.Add(tag);
            }

            if (!this.objectPools[tag].ContainsKey(name))
            {
                this.objectPools[tag].Add(name, new ObjectPolling(prefab.gameObject, 1, this.holder));
                this.names.Add(name);
            }


        }
    }

    private void LoadHolder()
    {
        if (this.holder != null) return;

        this.holder = UntilityHelper.EnsureChildTransform("Holder", gameObject);
        Debug.LogWarning($"{transform.name}: LoadHolder", gameObject);
    }

    public GameObject SpawnFronPool(string tag, string name, Vector2 position, Quaternion rotation)
    {
        ObjectPolling objectPolling = this.GetObjectPollingByTagAndName(tag, name);

        GameObject obj = objectPolling.SpawnObjectFromPool(position);
        obj.transform.rotation = rotation;

        return obj;
    }

    public void DespawnToPool(GameObject obj)
    {
        string tag = obj.transform.tag;
        string name = obj.transform.name;

        ObjectPolling objectPolling = this.GetObjectPollingByTagAndName(tag, name);

        Debug.Log(objectPolling == null);
        objectPolling.ReturnObjectToPool(obj);
    }

    protected ObjectPolling GetObjectPollingByTagAndName(string tag, string name)
    {
        if (!this.objectPools.ContainsKey(tag))
        {
            Debug.LogWarning($"Despawn Cannot found Tag:{tag} form objectPools");
            return null;
        }

        if (!this.objectPools[tag].ContainsKey(name))
        {
            Debug.LogWarning($"Despawn Cannot found Name:{name} form objectPools");
            return null;
        }

        return this.objectPools[tag][name];
    }
}