using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonoSingleton<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
{
    [Header("Singleton Pattern")]
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject(typeof(T).Name);
                instance = singletonObject.AddComponent<T>();
                DontDestroyOnLoad(singletonObject);
            }

            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this as T;
            if (transform.parent != null) transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        if (instance != this) Destroy(gameObject);
    }
}
