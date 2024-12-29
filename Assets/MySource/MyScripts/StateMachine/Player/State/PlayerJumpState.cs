using UnityEngine;

public class PlayerJumpState : IState, IObserveGrounded, IObserveWallSliding
{
    private PlayerController playerCtrl;
    private PlayerStateMachine playerState;
    private int remainJump;

    public PlayerJumpState(PlayerController _playerCtrl, PlayerStateMachine playerState)
    {
        this.playerCtrl = _playerCtrl;
        this.playerState = playerState;

        //Register Observe Grounded and Walled from Player
        this.playerCtrl.PlayerGroundedCheck.RegisterObserveGrounded(this);
        this.playerCtrl.PlayerWallSlidingCheck.RegisterObserveWallSliding(this);
    }

    public void Enter()
    {

        if (remainJump >= playerCtrl.PlayerDataSO.maxJump) return;

        float jumpForce = playerCtrl.PlayerDataSO.jumpForce;
        if (this.IsDoubleJump())
        {
            playerCtrl.anim.SetTrigger("doubleJump");
            playerCtrl.rb.velocity = Vector2.zero;
            playerCtrl.rb.AddForce(Vector2.up * jumpForce * 1.2f, ForceMode2D.Impulse);
            this.remainJump++;
            return;
        }
        playerCtrl.rb.velocity = Vector2.zero;
        playerCtrl.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // remainJump++;
    }

    public void Excute()
    {
        // if (playerState.TryJumpState()) return;
        if (playerState.TryWallSlideState())
        {
            this.ResetRemainJump();
            return;
        }
        if (playerState.TryMoveState()) return;
        if (playerState.TryIdleState()) return;
    }

    public void Exit()
    {
    }

    private bool IsWallJump()
    {
        return this.playerCtrl.PlayerWallSlidingCheck.IsWallSliding;
    }

    private bool IsDoubleJump()
    {
        return this.remainJump > 0;
    }

    private void ResetRemainJump()
    {
        this.remainJump = 0;
    }

    public void OnGroundedEnter()
    {
        this.ResetRemainJump();
        playerCtrl.anim.SetBool("isGrounded", true);
    }

    public void OnGroundedExit()
    {
        this.remainJump++;
        playerCtrl.anim.SetBool("isGrounded", false);
    }

    public void OnWallSlidingEnter()
    {
        this.ResetRemainJump();
    }

    public void OnWallSlidingExit()
    {
        this.remainJump++;
    }
}
