using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionDirectionChecker<T> where T : ICollisionDirection
{
    private ICheckCollisionLeft _collisionLeft;
    private ICheckCollisionRight _collisionRight;
    private ICheckCollisionUp _collisionUp;
    private ICheckCollisionDown _collisionDown;

    [SerializeField] private bool _isCollisionLeft;
    public bool IsCollisionLeft => _isCollisionLeft;
    [SerializeField] private bool _isCollisionRight;
    public bool IsCollisionRight => _isCollisionRight;
    [SerializeField] private bool _isCollisionUp;
    public bool IsCollisionUp => _isCollisionUp;
    [SerializeField] private bool _isCollisionDown;
    public bool IsCollisionDown => _isCollisionDown;

    public CollisionDirectionChecker()
    {
        CoroutineManager.Instance.StartManagedCoroutine(this.CheckingRoutine());
        this.InitializeCollisionCheckers();
    }

    private IEnumerator CheckingRoutine()
    {
        while (true)
        {
            if (this._collisionLeft != null)
                this._isCollisionLeft = this._collisionLeft.CheckCollisionLeft();

            if (this._collisionRight != null)
                this._isCollisionRight = this._collisionRight.CheckCollisionRight();

            if (this._collisionUp != null)
                this._isCollisionUp = this._collisionUp.CheckCollisionUp();

            if (this._collisionDown != null)
                this._isCollisionDown = this._collisionDown.CheckCollisionDown();

            this.OnChecking();

            yield return new WaitForSeconds(0.06f);
        }
    }

    private void InitializeCollisionCheckers()
    {
        if (this is ICheckCollisionLeft collisionLeft)
            this._collisionLeft = collisionLeft;
        if (this is ICheckCollisionRight collisionRight)
            this._collisionRight = collisionRight;
        if (this is ICheckCollisionUp collisionUp)
            this._collisionUp = collisionUp;
        if (this is ICheckCollisionDown collisionDown)
            this._collisionDown = collisionDown;
    }

    protected abstract void OnChecking();
}

public interface ICollisionDirection
{
}

public interface ICheckCollisionUp : ICollisionDirection
{
    public bool CheckCollisionUp();
}

public interface ICheckCollisionDown : ICollisionDirection
{
    public bool CheckCollisionDown();
}


public interface ICheckCollisionLeft : ICollisionDirection
{
    public bool CheckCollisionLeft();
}

public interface ICheckCollisionRight : ICollisionDirection
{
    public bool CheckCollisionRight();
}

