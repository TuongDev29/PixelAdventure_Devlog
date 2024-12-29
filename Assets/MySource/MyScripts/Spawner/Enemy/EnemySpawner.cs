using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class ensures that the EnemySpawner instance is available as a singleton for other classes to use.
public class EnemySpawner : Spawner<EEnemyCode>
{
    private static EnemySpawner _instance;
    public static EnemySpawner Instance => _instance;

    [Header("Spawning Stats")]
    [SerializeField] protected EnemySpawnPoint[] enemyPoints;
    [SerializeField] protected Vector2 lastCheckedPosition;
    [SerializeField] protected float spawnCheckInterval;
    [SerializeField] protected float spawnDistanceFromPlayer;

    protected override void Awake()
    {
        base.Awake();

        if (EnemySpawner._instance != null) Debug.LogWarning("Only one EnemySpawner allow to exist");
        EnemySpawner._instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEnemyPoints();
    }

    protected void LoadEnemyPoints()
    {
        if (this.enemyPoints.Length > 0) return;

        Transform spawnPointTransform = UntilityHelper.EnsureChildTransform("SpawnPoints", gameObject);
        enemyPoints = spawnPointTransform.GetComponentsInChildren<EnemySpawnPoint>();

        Debug.LogWarning(transform.name + "LoadEnemyPoints", gameObject);
    }

    protected virtual void Start()
    {
        this.lastCheckedPosition = PlayerManager.Instance.CurrentPosition * this.spawnCheckInterval;
    }

    protected virtual void FixedUpdate()
    {
        // this.Spawning();
    }

    // private void Spawning()
    // {
    //     Vector2 playerPosition = PlayerManager.Instance.CurrentPosition;
    //     if (Vector2.Distance(playerPosition, lastCheckedPosition) < this.spawnCheckInterval) return;

    //     foreach (EnemyPoint enemyPoint in this.enemyPoints)
    //     {
    //         if (Vector2.Distance(playerPosition, enemyPoint.transform.position) < spawnDistanceFromPlayer)
    //         {
    //             enemyPoint.SpawnEnemyAtPoint();
    //             continue;
    //         }

    //         enemyPoint.DespawnEnemyFromPoint();
    //     }

    //     this.lastCheckedPosition = playerPosition;
    // }
}