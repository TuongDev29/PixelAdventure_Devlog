using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInAttackRange : ICondition
{
    private readonly Blackboard<EEnemyBlackBoard> blackboard;
    private readonly EnemyController enemyCtrl;

    public IsPlayerInAttackRange(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.blackboard = blackboard;
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public void Enter()
    {

    }

    public bool Condition()
    {
        float attackHeightRange = 2f;
        float attackRange = enemyCtrl.EnemyData.attackRange;
        Vector2 playerPositon = this.blackboard.GetValue<Transform>(EEnemyBlackBoard.PlayerTransform).position;

        float distanceX = Mathf.Abs(enemyCtrl.transform.position.x - playerPositon.x);
        float distanceY = Mathf.Abs(enemyCtrl.transform.position.y - playerPositon.y);

        return distanceX <= attackRange && distanceY <= attackHeightRange;
    }
}