using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : BaseEnemyState
{
    private readonly Blackboard<EEnemyBlackBoard> blackboard;
    private readonly EnemyController enemyCtrl;
    private Vector2 currentPatrolPoint;

    public EnemyChaseState(Blackboard<EEnemyBlackBoard> blackboard) : base()
    {
        this.blackboard = blackboard;
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public override void Enter()
    {
        this.enemyCtrl.anim.SetBool("isRunning", true);
        this.currentPatrolPoint = this.GetPatrolPoint();
    }

    public override void Excute()
    {
        float moveDirection = (this.currentPatrolPoint.x - this.enemyCtrl.transform.position.x) > 0 ? 1 : -1;
        enemyCtrl.FacingHandler.FlipTowards(moveDirection);
        enemyCtrl.rb.velocity = new Vector2(moveDirection * enemyCtrl.EnemyData.runSpeed, enemyCtrl.rb.velocity.y);
    }

    public override void Exit()
    {
        this.enemyCtrl.anim.SetBool("isRunning", false);
        this.ResetVelocity();
    }

    private Vector2 GetPatrolPoint()
    {
        List<Vector2> patrolPoints = this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints);

        return patrolPoints[this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex)];
    }
    private void ResetVelocity()
    {
        Vector2 velocity = this.enemyCtrl.rb.velocity;
        velocity.x = 0;

        this.enemyCtrl.rb.velocity = velocity;
    }
}
