using System;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard<T> where T : Enum
{
    private Dictionary<T, object> data = new Dictionary<T, object>();
    private Dictionary<T, Type> dataTypes = new Dictionary<T, Type>();

    public void SetValue<C>(T key, C value)
    {
        data[key] = value;
        dataTypes[key] = typeof(C);
    }

    public C GetValue<C>(T key)
    {
        if (data.TryGetValue(key, out var value))
        {
            if (dataTypes.TryGetValue(key, out var expectedType) && expectedType == typeof(C))
            {
                return (C)value;
            }
            else
            {
                Debug.LogError($"[Blackboard]: Type mismatch for key-{key.ToString()}. Expected type: {expectedType}, but got: {typeof(T)}");
                return default;
            }
        }

        Debug.LogWarning($"[Blackboard]: key-{key.ToString()} does not exist");
        return default;
    }
}

public enum EEnemyBlackBoard
{
    EnemyController,
    PatrolPoints,
    PlayerTransform,
    EnemyTransform,
    PatrolIndex,
    MoveSpeed,
    IsHungry,
}