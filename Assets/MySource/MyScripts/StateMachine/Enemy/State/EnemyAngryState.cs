using System.Collections.Generic;
using UnityEngine;

public class EnemyAngryState : BaseEnemyState
{
    private readonly Blackboard<EEnemyBlackBoard> blackboard;
    private readonly EnemyController enemyCtrl;
    Vector2 currentPatrolPoint;

    public EnemyAngryState(Blackboard<EEnemyBlackBoard> blackboard) : base()
    {
        this.blackboard = blackboard;
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public override void Enter()
    {
        this.enemyCtrl.anim.SetBool("isAngry", true);
        this.currentPatrolPoint = this.GetPatrolPoint();
    }

    public override void Excute()
    {
        float moveDirection = (this.currentPatrolPoint.x - this.enemyCtrl.transform.position.x) > 0 ? 1 : -1;
        enemyCtrl.FacingHandler.FlipTowards(moveDirection);
        enemyCtrl.rb.velocity = new Vector2(moveDirection * enemyCtrl.EnemyData.runSpeed, enemyCtrl.rb.velocity.y);
        if (this.IsPatrolRange())
        {
            this.SelectNextPatrolPoint();
            this.currentPatrolPoint = this.GetPatrolPoint();
        }
    }

    public override void Exit()
    {
        this.enemyCtrl.anim.SetBool("isAngry", false);
        this.ResetVelocity();
    }

    private Vector2 GetPatrolPoint()
    {
        List<Vector2> patrolPoints = this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints);

        return patrolPoints[this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex)];
    }
    private bool IsPatrolRange()
    {
        Vector2 currentPatrolPoint = this.GetPatrolPoint();
        float distance = Mathf.Abs(currentPatrolPoint.x - this.enemyCtrl.transform.position.x);
        return distance <= 0.1;
    }

    private void SelectNextPatrolPoint()
    {
        int patrolIndex = this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex);
        this.blackboard.SetValue(EEnemyBlackBoard.PatrolIndex, (patrolIndex + 1) %
            this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints).Count);
    }

    private void ResetVelocity()
    {
        Vector2 velocity = this.enemyCtrl.rb.velocity;
        velocity.x = 0;

        this.enemyCtrl.rb.velocity = velocity;
    }
}
