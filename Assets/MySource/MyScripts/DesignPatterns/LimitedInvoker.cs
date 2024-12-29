
using System;
using System.Collections.Generic;

public class LimitedInvoker : Singleton<LimitedInvoker>
{
    private Dictionary<Action, int> invokerActions = new Dictionary<Action, int>();

    public void Invoke(Action function, int invokeNumber)
    {
        if (!invokerActions.ContainsKey(function))
        {
            invokerActions[function] = invokeNumber;
        }

        if (invokerActions[function] <= 0) return;

        function.Invoke();
        invokerActions[function] -= 1;
    }
}
