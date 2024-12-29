using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    private bool canTransition;
    public bool CanTransition => canTransition;
    public IState currStateAction;
    protected Dictionary<Enum, IState> states;
    private IEnumerator DelayTransitionAction;

    public StateMachine()
    {
        this.canTransition = true;
        this.states = RegisterState();

        CoroutineManager.Instance.StartManagedCoroutine(DelayInitialize());
    }

    protected virtual Dictionary<Enum, IState> RegisterState()
    {
        return new Dictionary<Enum, IState>();
    }

    protected void AddState(Enum stateKey, IState newState)
    {
        if (!states.ContainsKey(stateKey))
            this.states.Add(stateKey, newState);
        else
            Debug.LogWarning("StateKey: " + stateKey.ToString() + " to exits");
    }

    protected IState GetState(Enum stateKey)
    {

        if (this.states.ContainsKey(stateKey))
        {
            return states[stateKey];
        }
        else
            Debug.LogError("Not FOUND stateKey: " + stateKey.ToString());
        return null;
    }

    public virtual void ExcuteState()
    {
        this.currStateAction?.Excute();
    }

    public bool ChangeState(Enum stateKey)
    {
        if (!this.canTransition) return false;
        if (this.CompareState(stateKey)) return false;

        this.OnChangeState(stateKey);

        currStateAction?.Exit();
        states[stateKey].Enter();
        currStateAction = states[stateKey];
        return true;
    }

    protected virtual void OnChangeState(Enum statekey) { }

    public bool ChangeState(Enum stateKey, float delayTransitionTimer)
    {
        if (!this.ChangeState(stateKey)) return false;
        this.DelayTransition(delayTransitionTimer);
        return true;
    }

    public void DelayTransition(float timer)
    {
        if (this.DelayTransitionAction != null)
            CoroutineManager.Instance.StopManagedCoroutine(this.DelayTransitionAction);

        this.DelayTransitionAction = StartDelayTransition(timer);
        CoroutineManager.Instance.StartManagedCoroutine(DelayTransitionAction);
    }

    public bool CompareState(Enum stateKey)
    {
        if (this.states.ContainsKey(stateKey) && this.currStateAction == states[stateKey])
            return true;
        return false;
    }

    public bool ContainState(Enum stateKey)
    {
        return this.states.ContainsKey(stateKey);
    }

    public void EnableTransition()
    {
        if (this.DelayTransitionAction != null)
            CoroutineManager.Instance.StopManagedCoroutine(this.DelayTransitionAction);
        this.canTransition = true;
    }

    public void DisableTransition()
    {
        if (this.DelayTransitionAction != null)
            CoroutineManager.Instance.StopManagedCoroutine(this.DelayTransitionAction);
        this.canTransition = false;
    }

    private IEnumerator StartDelayTransition(float timer)
    {
        this.canTransition = false;

        yield return new WaitForSeconds(timer);
        this.canTransition = true;
    }

    private IEnumerator DelayInitialize()
    {
        yield return null;
        this.InitializeState();
    }
    protected abstract void InitializeState();
}
