using System;
using UnityEngine;

public class UntilityHelper
{
    public static T TryParseEnum<T>(string value)
    {
        if (Enum.TryParse(typeof(T), value, true, out object result) && result is T enumResult)
        {
            return enumResult;
        }

        Debug.LogWarning($"The value '{value}' is not defined in the {typeof(T).Name} enum.");
        return default;
    }

    public static void AutoFetchComponent<T>(ref T _object, GameObject gameObject) where T : Component
    {
        if (_object != null) return;
        _object = gameObject.transform.GetComponent<T>();
        Debug.LogWarning($"{gameObject.transform.name}: Load => {typeof(T)}", gameObject);
    }

    public static void AutoFetchParentComponent<T>(ref T _object, GameObject gameObject) where T : Component
    {
        if (_object != null) return;
        _object = gameObject.transform.parent.GetComponent<T>();
        Debug.LogWarning($"{gameObject.transform.name}: LoadParent => {typeof(T)}", gameObject);
    }

    public static void AutoFetchChildComponent<T>(ref T _object, GameObject gameObject) where T : Component
    {
        if (_object != null) return;
        _object = gameObject.transform.GetComponentInChildren<T>();
        Debug.LogWarning($"{gameObject.transform.name}: LoadChild => {typeof(T)}", gameObject);
    }

    public static Transform EnsureChildTransform(string name, GameObject gameObject)
    {
        Transform transform = gameObject.transform;
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
