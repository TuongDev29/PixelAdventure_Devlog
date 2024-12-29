using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInRange : ICondition
{
    private readonly Blackboard<EEnemyBlackBoard> blackboard;
    private readonly EnemyController enemyCtrl;

    public IsPlayerInRange(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.blackboard = blackboard;
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public void Enter()
    {

    }

    public bool Condition()
    {
        Vector2 playerPosition = this.blackboard.GetValue<Transform>(EEnemyBlackBoard.PlayerTransform).position;
        Vector2 enemyPosition = this.enemyCtrl.transform.position;
        Vector2 currentPatrolPoint = this.GetPatrolPoint();

        float minX = Mathf.Min(enemyPosition.x, currentPatrolPoint.x);
        float maxX = Mathf.Max(enemyPosition.x, currentPatrolPoint.x);

        return playerPosition.x >= minX && playerPosition.x <= maxX;
    }

    private Vector2 GetPatrolPoint()
    {
        List<Vector2> patrolPoints = this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints);

        return patrolPoints[this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex)];
    }
}