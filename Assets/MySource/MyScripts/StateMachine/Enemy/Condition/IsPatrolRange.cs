using System.Collections.Generic;
using UnityEngine;

public class IsPatrolRange : ICondition
{
    private readonly Blackboard<EEnemyBlackBoard> blackboard;
    private readonly EnemyController enemyCtrl;
    private Vector2 CurrPatrolPoint;

    public IsPatrolRange(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.blackboard = blackboard;
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public void Enter()
    {
        this.CurrPatrolPoint = this.GetPatrolPoint();
    }

    public bool Condition()
    {
        float distance = Mathf.Abs(this.CurrPatrolPoint.x - this.enemyCtrl.transform.position.x);
        return distance <= 0.1;
    }

    private Vector2 GetPatrolPoint()
    {
        List<Vector2> patrolPoints = this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints);

        return patrolPoints[this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex)];
    }
}