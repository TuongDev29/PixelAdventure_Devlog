public class InverseCondition : ICondition
{
    private readonly ICondition condition;

    public InverseCondition(ICondition condition)
    {
        this.condition = condition;
    }

    public bool Condition()
    {
        return !this.condition.Condition();
    }

    public void Enter()
    {
        this.condition.Enter();
    }
}