using System.Collections.Generic;
using UnityEngine;

public class IsAlive : ICondition
{
    private readonly EnemyController enemyCtrl;

    public IsAlive(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public void Enter()
    {

    }

    public bool Condition()
    {
        return !this.enemyCtrl.EnemyDamageable.IsDead;
    }
}