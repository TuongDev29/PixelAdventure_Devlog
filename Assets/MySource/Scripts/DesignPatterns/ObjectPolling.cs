using System.Collections.Generic;
using UnityEngine;

namespace DevLog
{
    public class ObjectPolling
    {
        private GameObject prefab;
        private Queue<GameObject> poolQueue;
        private Transform parentTransform;

        public ObjectPolling(GameObject _prefab, int initialSize = 1, Transform parent = null)
        {
            poolQueue = new Queue<GameObject>();

            this.prefab = _prefab;
            this.prefab.SetActive(false);
            this.parentTransform = parent;

            for (int i = 0; i < initialSize; i++)
            {
                this.AddObjectToPool();
            }
        }

        public GameObject SpawnObjectFromPool(Vector2 position)
        {
            if (this.poolQueue.Count == 0)
            {
                this.AddObjectToPool();
            }

            GameObject obj = this.poolQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;

            return obj;
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }

        private void AddObjectToPool()
        {
            GameObject obj = Object.Instantiate(prefab, parentTransform);
            obj.transform.name = prefab.transform.name;
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

}