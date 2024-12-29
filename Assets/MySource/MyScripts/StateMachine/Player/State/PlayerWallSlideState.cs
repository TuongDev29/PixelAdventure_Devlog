using UnityEngine;

public class PlayerWallSlideState : IState
{
    private PlayerController playerCtrl;
    private PlayerStateMachine playerState;
    protected float wallSlidingSpeed = 1f;

    public PlayerWallSlideState(PlayerController _playerCtrl, PlayerStateMachine playerState)
    {
        this.playerCtrl = _playerCtrl;
        this.playerState = playerState;
    }

    public void Enter()
    {
        playerCtrl.anim.SetBool("isWallSliding", true);
    }

    public void Excute()
    {
        //Prioritize jump state
        if (playerState.TryJumpState()) return;

        //Wallsliding handle
        playerCtrl.rb.velocity = new Vector2(0, -wallSlidingSpeed);

        playerCtrl.FacingHandler.FlipTowards(playerCtrl.PlayerWallSlidingCheck.IsCollisionRight);

        //Try change state
        float moveInput = InputManager.Instance.MoveInput();
        bool canMove = Mathf.Abs(moveInput) > 0.25f &&
            playerCtrl.PlayerWallSlidingCheck.IsCollisionRight != moveInput > 0;
        if (canMove && playerState.TryMoveState())
        {
            playerState.DelayTransition(0.02f);
            return;
        }
        if (playerState.TryIdleState()) return;
        if (CheckWallSliding()) return;
    }

    public void Exit()
    {
        playerCtrl.anim.SetBool("isWallSliding", false);
    }

    public void OnGroundedExit()
    {
    }

    public void OnGroundedStay()
    {
    }

    private bool CheckWallSliding()
    {
        bool isWallSliding = playerCtrl.PlayerWallSlidingCheck.IsWallSliding;

        if (!isWallSliding)
        {
            playerState.ChangeState(EPlayerState.Idle);
        }

        return isWallSliding;
    }
}
