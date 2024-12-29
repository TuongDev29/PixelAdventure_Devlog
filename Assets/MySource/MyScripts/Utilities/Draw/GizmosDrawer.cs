using System;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDrawer : Singleton<GizmosDrawer>
{
    protected List<Action> drawActions = new List<Action>();

    protected void OnDrawGizmos()
    {
        foreach (Action action in drawActions)
        {
            action.Invoke();
        }
    }

    public void AddDrawAction(Action action)
    {
        this.drawActions.Add(action);
    }
}