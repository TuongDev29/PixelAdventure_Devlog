using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateMachine : StateMachine
{
    protected readonly Blackboard<EEnemyBlackBoard> blackboard;
    protected abstract EEnemyState DefaultState { get; }
    private BaseEnemyState baseEnemyState;
    protected ICondition OnlyTrue = new OnlyTrue();

    public EnemyStateMachine(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.blackboard = blackboard;

        CoroutineManager.Instance.StartManagedCoroutine(this.DelayInitializeTransition());
    }

    protected override void OnChangeState(Enum statekey)
    {
        base.OnChangeState(statekey);
        this.baseEnemyState = this.states[statekey] as BaseEnemyState;
        this.baseEnemyState.EnterTransitions();
    }

    public override void ExcuteState()
    {
        base.ExcuteState();
        this.ExcuteTransition();
    }

    private void ExcuteTransition()
    {
        if (this.baseEnemyState == null) return;

        EEnemyState eEnemyState = baseEnemyState.CheckTransitions();
        if (eEnemyState != EEnemyState.None)
        {
            if (this.CompareState(eEnemyState)) return;
            this.ChangeState(eEnemyState);
        }
    }

    public void AddTransitionForState(EEnemyState fromState, EEnemyState toState, ICondition conditionAction = default, Action onTransitionAction = null)
    {
        if (conditionAction == default) conditionAction = this.OnlyTrue;
        ((BaseEnemyState)this.states[fromState]).AddTransition(new StateTransition<EEnemyState>(conditionAction, toState, onTransitionAction));
    }

    List<EEnemyState> listStatesTransitionForAll = new List<EEnemyState>();
    public void AddTransitionForAllStates(EEnemyState toState, ICondition conditionAction)
    {
        this.listStatesTransitionForAll.Add(toState);
        foreach (var stateKey in this.states.Keys)
        {
            if (this.CheckStateInListTransitionForAll((EEnemyState)stateKey)) continue;

            BaseEnemyState baseEnemyState = this.states[stateKey] as BaseEnemyState;
            baseEnemyState.AddTransition(new StateTransition<EEnemyState>(conditionAction, toState));
        }
    }

    private bool CheckStateInListTransitionForAll(EEnemyState enemyState)
    {
        foreach (var item in this.listStatesTransitionForAll)
        {
            if (item == enemyState) return true;
        }
        return false;
    }

    protected virtual void InitializeTransition() { }

    private IEnumerator DelayInitializeTransition()
    {
        yield return null;
        this.InitializeTransition();

        this.ChangeState(this.DefaultState);
    }
}
