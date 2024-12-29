using System.Collections.Generic;
using UnityEngine;

public abstract class WallSlidingCheck<T> : WalledCheck<T> where T : class, ICheckCollisionLeft, ICheckCollisionRight
{
    private bool previousWallSlidingState;
    [SerializeField] private bool _isWallSliding;
    public bool IsWallSliding => _isWallSliding;
    private List<IObserveWallSliding> observeWallSlidings;

    protected WallSlidingCheck() : base()
    {
        observeWallSlidings = new List<IObserveWallSliding>();
    }

    public void RegisterObserveWallSliding(IObserveWallSliding observeWallSliding)
    {
        if (this.observeWallSlidings.Contains(observeWallSliding)) return;
        this.observeWallSlidings.Add(observeWallSliding);
    }

    protected override void OnChecking()
    {
        base.OnChecking();
        this._isWallSliding = CheckWallSliding();

        if (this.IsWallSliding != this.previousWallSlidingState)
        {
            if (this.IsWallSliding)
                this.NotifyWallSlidingEnter();
            else
                this.NotifyWallSlidingExit();

            this.previousWallSlidingState = this.IsWallSliding;
        }
    }

    protected abstract bool CheckWallSliding();

    protected virtual void NotifyWallSlidingEnter()
    {
        foreach (IObserveWallSliding observeWallSliding in this.observeWallSlidings)
        {
            observeWallSliding.OnWallSlidingEnter();
        }
    }
    protected virtual void NotifyWallSlidingExit()
    {
        foreach (IObserveWallSliding observeWallSliding in this.observeWallSlidings)
        {
            observeWallSliding.OnWallSlidingExit();
        }
    }
    protected virtual void NotifyWallSliding() { }
}
