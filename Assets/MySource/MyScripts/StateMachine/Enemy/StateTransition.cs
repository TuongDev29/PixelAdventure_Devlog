using System;

public class StateTransition<T> where T : Enum
{
    private ICondition condition;
    private Action onTransitionAction;
    public T nextState { private set; get; }

    public StateTransition(ICondition condition, T nextState, Action onTransitionAction = null)
    {
        this.condition = condition;
        this.nextState = nextState;
        this.onTransitionAction = onTransitionAction;
    }

    public void EnterTransition()
    {
        this.condition.Enter();
    }

    public bool ShouldTransition()
    {
        bool result = this.condition.Condition();
        if (result) this.onTransitionAction?.Invoke();
        return result;
    }
}
