using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    private static DontDestroyOnLoadObject instance;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;

        if (transform.parent != null) transform.SetParent(null);

        DontDestroyOnLoad(gameObject);
    }
}