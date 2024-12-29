using System.Collections.Generic;
using UnityEngine;

public abstract class GroundedCheck<T> : CollisionDirectionChecker<T> where T : class, ICheckCollisionDown
{
    public bool IsGrounded
    {
        get
        {
            return IsCollisionDown;
        }
    }
    private bool previousGroundedState;
    private List<IObserveGrounded> observeGroundeds;

    protected GroundedCheck() : base()
    {
        this.previousGroundedState = false;
        this.observeGroundeds = new List<IObserveGrounded>();
    }

    public void RegisterObserveGrounded(IObserveGrounded observeGrounded)
    {
        if (observeGroundeds.Contains(observeGrounded)) return;
        observeGroundeds.Add(observeGrounded);
    }

    protected override void OnChecking()
    {
        if (this.IsGrounded != this.previousGroundedState)
        {
            if (this.IsGrounded)
                this.NotifyGroundedEnter();
            else
                this.NotifyGroundedExit();

            //Just update on change State
            this.previousGroundedState = this.IsGrounded;
        }
    }

    private void NotifyGroundedEnter()
    {
        foreach (IObserveGrounded observeGrounded in observeGroundeds)
        {
            observeGrounded.OnGroundedEnter();
        }
    }

    private void NotifyGroundedExit()
    {
        foreach (IObserveGrounded observeGrounded in observeGroundeds)
        {
            observeGrounded.OnGroundedExit();
        }
    }
}
