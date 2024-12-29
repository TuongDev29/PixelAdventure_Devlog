using System.Collections.Generic;

public abstract class BaseEnemyState : IState
{
    protected List<StateTransition<EEnemyState>> transitions;

    public BaseEnemyState()
    {
        this.transitions = new List<StateTransition<EEnemyState>>();
    }

    public void AddTransition(StateTransition<EEnemyState> stateTransition)
    {
        this.transitions.Add(stateTransition);
    }

    public EEnemyState CheckTransitions()
    {
        foreach (var transition in this.transitions)
        {
            if (transition.ShouldTransition())
            {
                return transition.nextState;
            }
        }
        return EEnemyState.None;
    }

    public void EnterTransitions()
    {
        foreach (var transition in this.transitions)
        {
            transition.EnterTransition();
        }
    }

    public virtual void Enter() { }
    public virtual void Excute() { }
    public virtual void Exit() { }
}
