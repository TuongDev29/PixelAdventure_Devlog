using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : BaseSpawnPoint<EEnemyCode>
{
    public List<Vector2> PatrolPoints;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPatrolPoints();

        this.spawnTag = ETagCode.Enemy;
    }

    protected void Start()
    {
        this.Spawn();
    }

    protected void LoadPatrolPoints()
    {
        if (this.PatrolPoints.Count > 0) return;

        Transform leftBoundary = UntilityHelper.EnsureChildTransform("LeftBoundary", gameObject);
        Transform rightBoundary = UntilityHelper.EnsureChildTransform("RightBoundary", gameObject);

        this.PatrolPoints.Add(leftBoundary.position);
        this.PatrolPoints.Add(rightBoundary.position);

        Debug.LogWarning(transform.name + "LoadPatrolPoints", gameObject);
    }

    public override GameObject Spawn()
    {
        GameObject obj = base.Spawn();

        EnemyAI enemyAI = obj.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.Initialize(this.PatrolPoints);
        }

        return obj;
    }
}