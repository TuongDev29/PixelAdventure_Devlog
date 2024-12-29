using System;
using System.Collections.Generic;
using UnityEngine;

public class TrunkStateMachine : EnemyStateMachine
{
    protected override EEnemyState DefaultState => EEnemyState.Patrol;

    public TrunkStateMachine(Blackboard<EEnemyBlackBoard> blackboard) : base(blackboard)
    {
    }

    protected override void InitializeState()
    {
        this.AddState(EEnemyState.Idle, new EnemyIdleState());
        this.AddState(EEnemyState.Patrol, new EnemyPatrolState(this.blackboard));
        this.AddState(EEnemyState.Shoot, new EnemyShootState(this.blackboard));
        this.AddState(EEnemyState.Dead, new EnemyDeadState(this.blackboard));
    }

    protected override void InitializeTransition()
    {
        //Initial Condition
        ICondition IsPatrolRange = new IsPatrolRange(this.blackboard);
        ICondition IsPlayerInAttackRange = new IsPlayerInAttackRange(this.blackboard);
        ICondition IsDead = new IsDead(this.blackboard);
        ICondition IsAlive = new IsAlive(this.blackboard);
        ICondition Cooldown = new CooldownCondition(0.8f);
        ICondition ShootCooldown = new CooldownCondition(1.2f);

        //Transition for ALl state
        this.AddTransitionForAllStates(EEnemyState.Dead, IsDead);

        //Transition for Dead State
        this.AddTransitionForState(EEnemyState.Dead, EEnemyState.Idle, IsAlive);
        //Transition for Idle State
        this.AddTransitionForState(EEnemyState.Idle, EEnemyState.Patrol, Cooldown);
        //Transition for Patrol State
        this.AddTransitionForState(EEnemyState.Patrol, EEnemyState.Shoot, IsPlayerInAttackRange);
        this.AddTransitionForState(EEnemyState.Patrol, EEnemyState.Idle, IsPatrolRange, this.SelectNextPatrolPoint);
        //Transition for Shoot State
        this.AddTransitionForState(EEnemyState.Shoot, EEnemyState.Idle, ShootCooldown);
    }

    private void SelectNextPatrolPoint()
    {
        int patrolIndex = this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex);
        this.blackboard.SetValue(EEnemyBlackBoard.PatrolIndex, (patrolIndex + 1) %
            this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints).Count);
    }
}
