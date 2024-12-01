using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevLog
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
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
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                if (transform.parent != null) transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }

            if (instance != this) Destroy(gameObject);
        }
    }

}