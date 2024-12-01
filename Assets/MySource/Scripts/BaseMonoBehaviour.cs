using UnityEngine;

namespace DevLog
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        protected virtual void Reset()
        {
            this.ControllerLoader();
        }

        protected virtual void Awake()
        {
            this.ControllerLoader();
        }

        protected void ControllerLoader()
        {
            this.Begin();
            this.LoadComponent();
            this.ResetValue();
            this.EndLoad();
        }

        protected virtual void Begin() { }

        protected virtual void LoadComponent() { }

        protected virtual void ResetValue() { }

        protected virtual void EndLoad() { }

        protected void AutoLoadComponent<T>(ref T _object, T _objectValue)
        {
            if (_object != null) return;
            _object = _objectValue;
            Debug.LogWarning("AutoLoad => " + typeof(T), gameObject);
        }

        protected Transform EnsureChildTransform(string name)
        {
            Transform childTransform = transform.Find(name);
            if (childTransform == null)
            {
                GameObject newSpawnPoints = new GameObject(name);
                newSpawnPoints.transform.SetParent(transform);
                childTransform = newSpawnPoints.transform;

                // Log a warning message
                Debug.LogWarning(transform.parent + $" SpawnPoints not found. A new GameObject named '{name}' has been created.", gameObject);
            }

            return childTransform;
        }
    }

}