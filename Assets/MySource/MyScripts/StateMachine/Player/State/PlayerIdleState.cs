
using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController playerCtrl;
    private PlayerStateMachine playerState;

    public PlayerIdleState(PlayerController _playerCtrl, PlayerStateMachine playerState)
    {
        this.playerCtrl = _playerCtrl;
        this.playerState = playerState;
    }

    public void Enter()
    {
        Vector2 velocity = playerCtrl.rb.velocity;
        velocity.x = 0;

        playerCtrl.rb.velocity = velocity;
    }

    public void Excute()
    {
        if (playerState.TryMoveState()) return;
        if (playerState.TryJumpState()) return;
        if (playerState.TryWallSlideState()) return;

        this.ReduceSpeed(0.94f);
    }

    private void ReduceSpeed(float factor)
    {
        // Reduce speed
        Vector2 velocity = playerCtrl.rb.velocity;
        velocity.x *= factor;
        playerCtrl.rb.velocity = velocity;
    }

    public void Exit()
    {
    }
}
