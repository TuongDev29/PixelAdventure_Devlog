using UnityEngine;

public class EffectSpawner : Spawner<EEffectCode>
{
    private static EffectSpawner _instance;
    public static EffectSpawner Instance => _instance;

    protected override void Awake()
    {
        base.Awake();

        if (EffectSpawner._instance != null) Debug.LogWarning("Only one EffectSpawner allow to exist");
        EffectSpawner._instance = this;
    }
}

public enum EEffectCode
{
    BreakBox1,
    BreakBox2,
    BreakBox3,
    BreakBullet1
}