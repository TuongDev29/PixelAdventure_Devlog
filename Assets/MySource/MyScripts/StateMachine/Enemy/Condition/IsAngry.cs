using System.Collections.Generic;
using UnityEngine;

public class IsAngry : ICondition
{
    private readonly EnemyController enemyCtrl;

    public IsAngry(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public void Enter()
    {

    }

    public bool Condition()
    {
        return this.enemyCtrl.EnemyDamageable.Health < this.enemyCtrl.EnemyDamageable.MaxHealth;
    }
}