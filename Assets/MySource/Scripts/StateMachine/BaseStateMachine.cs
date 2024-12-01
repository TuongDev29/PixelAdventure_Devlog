using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevLog;

namespace DevLog
{
    public abstract class BaseStateMachine
    {
        private bool canTransition;
        public bool CanTransition => canTransition;
        public IState stateAction;
        protected Dictionary<Enum, IState> state;
        private IEnumerator DelayTransitionAction;
        private MonoBehaviour monoBehaviour;

        public BaseStateMachine(MonoBehaviour monoBehaviour)
        {
            this.monoBehaviour = monoBehaviour;
            this.canTransition = true;
            this.state = RegisterState();

            monoBehaviour.StartCoroutine(DelayInitializeState());
        }

        protected virtual Dictionary<Enum, IState> RegisterState()
        {
            return new Dictionary<Enum, IState>();
        }

        protected void AddState(Enum stateKey, IState newState)
        {
            if (!state.ContainsKey(stateKey))
                this.state.Add(stateKey, newState);
            else
                Debug.LogWarning("StateKey: " + stateKey.ToString() + " to exits");
        }

        protected IState GetState(Enum stateKey)
        {

            if (this.state.ContainsKey(stateKey))
            {
                return state[stateKey];
            }
            else
                Debug.LogError("Not FOUND stateKey: " + stateKey.ToString());
            return null;
        }

        public virtual void ExcuteState()
        {
            this.stateAction?.Excute();
        }

        public void ChangeState(Enum stateKey)
        {
            if (!this.canTransition) return;
            if (this.CompareState(stateKey)) return;

            this.DebugOnChangeState(stateKey);

            stateAction?.Exit();
            state[stateKey].Enter();
            stateAction = state[stateKey];
        }

        protected virtual void DebugOnChangeState(Enum statekey) { }

        public void ChangeState(Enum stateKey, float delayTransitionTimer)
        {
            if (!this.canTransition) return;

            this.ChangeState(stateKey);
            this.DelayTransition(delayTransitionTimer);
        }

        public void DelayTransition(float timer)
        {
            if (this.DelayTransitionAction != null)
                monoBehaviour.StopCoroutine(this.DelayTransitionAction);

            this.DelayTransitionAction = StartDelayTransition(timer);
            monoBehaviour.StartCoroutine(DelayTransitionAction);
        }

        public bool CompareState(Enum stateKey)
        {
            if (this.state.ContainsKey(stateKey) && this.stateAction == state[stateKey])
                return true;
            return false;
        }

        public bool ContainState(Enum stateKey)
        {
            return this.state.ContainsKey(stateKey);
        }

        public void EnableTransition()
        {
            if (this.DelayTransitionAction != null)
                monoBehaviour.StopCoroutine(this.DelayTransitionAction);
            this.canTransition = true;
        }

        public void DisableTransition()
        {
            if (this.DelayTransitionAction != null)
                monoBehaviour.StopCoroutine(this.DelayTransitionAction);
            this.canTransition = false;
        }

        private IEnumerator StartDelayTransition(float timer)
        {
            this.canTransition = false;

            yield return new WaitForSeconds(timer);
            this.canTransition = true;
        }

        private IEnumerator DelayInitializeState()
        {
            yield return null;
            this.InitializeState();
        }
        protected abstract void InitializeState();
    }

}