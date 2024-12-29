using System.Collections.Generic;
using UnityEngine;

public class IsDead : ICondition
{
    private readonly EnemyController enemyCtrl;

    public IsDead(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public void Enter()
    {

    }

    public bool Condition()
    {
        return this.enemyCtrl.EnemyDamageable.IsDead;
    }
}