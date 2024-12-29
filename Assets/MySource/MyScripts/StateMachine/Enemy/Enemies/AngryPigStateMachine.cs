using System;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigStateMachine : EnemyStateMachine
{
    protected override EEnemyState DefaultState => EEnemyState.Idle;

    public AngryPigStateMachine(Blackboard<EEnemyBlackBoard> blackboard) : base(blackboard)
    {
    }
    protected override void InitializeState()
    {
        this.AddState(EEnemyState.Idle, new EnemyIdleState());
        this.AddState(EEnemyState.Patrol, new EnemyPatrolState(this.blackboard));
        this.AddState(EEnemyState.Angry, new EnemyAngryState(this.blackboard));
        this.AddState(EEnemyState.Dead, new EnemyDeadState(this.blackboard));
    }

    protected override void InitializeTransition()
    {
        //Initial Condition
        ICondition IsPlayerInAttackRange = new IsPlayerInAttackRange(this.blackboard);
        ICondition IsPatrolRange = new IsPatrolRange(this.blackboard);
        ICondition IsPlayerInRange = new IsPlayerInRange(this.blackboard);
        ICondition Cooldown = new CooldownCondition(0.8f);
        ICondition IsAngry = new IsAngry(this.blackboard);
        ICondition IsDead = new IsDead(this.blackboard);
        ICondition IsNotAngry = new InverseCondition(IsAngry);

        //Transition for all states
        this.AddTransitionForAllStates(EEnemyState.Dead, IsDead);
        this.AddTransitionForAllStates(EEnemyState.Angry, IsAngry);

        //Transiton for Idle State
        this.AddTransitionForState(EEnemyState.Idle, EEnemyState.Patrol, Cooldown);
        //Transiton for Patrol State
        this.AddTransitionForState(EEnemyState.Patrol, EEnemyState.Idle, IsPatrolRange, this.SelectNextPatrolPoint);
        //Transiton for Attack State
        this.AddTransitionForState(EEnemyState.Angry, EEnemyState.Idle, new InverseCondition(IsAngry));
    }

    private void SelectNextPatrolPoint()
    {
        int patrolIndex = this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex);
        this.blackboard.SetValue(EEnemyBlackBoard.PatrolIndex, (patrolIndex + 1) %
            this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints).Count);
    }

    //IsPlayerNearby
}

/*
private bool HandlePatrolPointSelection()
    {
        if (this.IsPatrolRange())
        {
            this.SelectNextPatrolPoint();
            return true;
        }
        return false;
    }

    private bool IsAngry()
    {
        float health = enemyCtrl.EnemyDamageable.Health;
        float maxHealth = enemyCtrl.EnemyDamageable.MaxHealth;
        return health < maxHealth;
    }

    private bool IsPatrolRange()
    {
        Vector2 currentPatrolPoint = this.GetPatrolPoint();
        float distance = Mathf.Abs(currentPatrolPoint.x - this.enemyCtrl.transform.position.x);
        return distance <= 0.1;
    }

    private bool IsPlayerInRange()
    {
        Vector2 playerPosition = this.blackboard.GetValue<Transform>(EEnemyBlackBoard.PlayerTransform).position;
        Vector2 enemyPosition = this.enemyCtrl.transform.position;
        Vector2 currentPatrolPoint = this.GetPatrolPoint();

        float minX = Mathf.Min(enemyPosition.x, currentPatrolPoint.x);
        float maxX = Mathf.Max(enemyPosition.x, currentPatrolPoint.x);

        return playerPosition.x >= minX && playerPosition.x <= maxX;
    }

    private bool IsPlayerInAttackRange()
    {
        float attackHeightRange = 2f;
        float attackRange = enemyCtrl.EnemyData.attackRange;
        Vector2 playerPositon = this.blackboard.GetValue<Transform>(EEnemyBlackBoard.PlayerTransform).position;

        float distanceX = Mathf.Abs(enemyCtrl.transform.position.x - playerPositon.x);
        float distanceY = Mathf.Abs(enemyCtrl.transform.position.y - playerPositon.y);

        return distanceX <= attackRange && distanceY <= attackHeightRange;
    }

    private Vector2 GetPatrolPoint()
    {
        List<Vector2> patrolPoints = this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints);

        return patrolPoints[this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex)];
    }

    private void SelectNextPatrolPoint()
    {
        int patrolIndex = this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex);
        this.blackboard.SetValue(EEnemyBlackBoard.PatrolIndex, (patrolIndex + 1) %
            this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints).Count);
    }
*/
