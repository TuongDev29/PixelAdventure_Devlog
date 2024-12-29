using System;
using UnityEngine;

public class EnemyDeadState : BaseEnemyState
{
    private readonly EnemyController enemyCtrl;

    public EnemyDeadState(Blackboard<EEnemyBlackBoard> blackboard) : base()
    {
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public override void Enter()
    {
        this.enemyCtrl.anim.SetBool("isDead", true);
    }

    public override void Exit()
    {
        this.enemyCtrl.anim.SetBool("isDead", true);
    }

    public override void Excute()
    {
        base.Excute();
    }
}
