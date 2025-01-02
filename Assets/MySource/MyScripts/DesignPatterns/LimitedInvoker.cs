
using System;

public class LimitedInvoker
{
    private Action action;
    private int invokeNumber;

    public LimitedInvoker(Action action, int invokeNumber = 0)
    {
        this.action = action;
        this.invokeNumber = invokeNumber;
    }

    public void IncreateInvoke()
    {
        this.invokeNumber += 1;
    }

    public void AddInvokeNumber(int num)
    {
        this.invokeNumber = num;
    }

    public void Invoke()
    {
        if (this.invokeNumber <= 0) return;
        this.action.Invoke();
        this.invokeNumber--;
    }
}
