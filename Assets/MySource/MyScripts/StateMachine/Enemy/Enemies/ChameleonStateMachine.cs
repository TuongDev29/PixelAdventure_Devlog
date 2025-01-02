using System;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonStateMachine : EnemyStateMachine
{
    protected override EEnemyState DefaultState => EEnemyState.Idle;

    public ChameleonStateMachine(Blackboard<EEnemyBlackBoard> blackboard) : base(blackboard)
    {
    }

    protected override void InitializeState()
    {
        this.AddState(EEnemyState.Idle, new EnemyIdleState());
        this.AddState(EEnemyState.Patrol, new EnemyPatrolState(this.blackboard));
        this.AddState(EEnemyState.Chase, new EnemyChaseState(this.blackboard));
        this.AddState(EEnemyState.Attack, new EnemyAttackState(this.blackboard));
        this.AddState(EEnemyState.Dead, new EnemyDeadState(this.blackboard));
    }

    protected override void InitializeTransition()
    {
        //Initial Condition
        ICondition IsPlayerInAttackRange = new IsPlayerInAttackRange(this.blackboard);
        ICondition IsPatrolRange = new IsPatrolRange(this.blackboard);
        ICondition IsPlayerInRange = new IsPlayerInRange(this.blackboard);
        ICondition IsDead = new IsDead(this.blackboard);
        ICondition IsAlive = new IsAlive(this.blackboard);
        ICondition Cooldown = new CooldownCondition(0.8f);
        ICondition AttackCooldown = new CooldownCondition(1.2f);
        ICondition IsNotAttackRange = new InverseCondition(IsPlayerInAttackRange);

        //Transition for All State
        this.AddTransitionForAllStates(EEnemyState.Dead, IsDead);

        //Transition for Dead State
        this.AddTransitionForState(EEnemyState.Dead, EEnemyState.Idle, IsAlive);
        //Transition for Idle State
        this.AddTransitionForState(EEnemyState.Idle, EEnemyState.Patrol, Cooldown);
        this.AddTransitionForState(EEnemyState.Idle, EEnemyState.Attack, IsPlayerInAttackRange);
        //Transition for Patrol State
        this.AddTransitionForState(EEnemyState.Patrol, EEnemyState.Attack, IsPlayerInAttackRange);
        this.AddTransitionForState(EEnemyState.Patrol, EEnemyState.Idle, IsPatrolRange, this.SelectNextPatrolPoint);
        this.AddTransitionForState(EEnemyState.Patrol, EEnemyState.Chase, IsPlayerInRange);
        //Transition for Chase State
        // this.AddTransitionForState(EEnemyState.Chase, EEnemyState.Attack, IsPlayerInAttackRange);
        this.AddTransitionForState(EEnemyState.Chase, EEnemyState.Idle, IsPatrolRange, this.SelectNextPatrolPoint);
        this.AddTransitionForState(EEnemyState.Chase, EEnemyState.Attack, IsPlayerInAttackRange);
        //Transition for Attack State
        this.AddTransitionForState(EEnemyState.Attack, EEnemyState.Idle, AttackCooldown);
    }

    private void SelectNextPatrolPoint()
    {
        int patrolIndex = this.blackboard.GetValue<int>(EEnemyBlackBoard.PatrolIndex);
        this.blackboard.SetValue(EEnemyBlackBoard.PatrolIndex, (patrolIndex + 1) %
            this.blackboard.GetValue<List<Vector2>>(EEnemyBlackBoard.PatrolPoints).Count);
    }
}
