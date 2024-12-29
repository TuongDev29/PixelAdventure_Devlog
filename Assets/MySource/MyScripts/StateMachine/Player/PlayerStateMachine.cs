using System.Collections;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private PlayerController playerCtrl;

    public PlayerStateMachine(PlayerController playerController)
    {
        this.playerCtrl = playerController;
    }

    protected override void InitializeState()
    {
        this.AddState(EPlayerState.Idle, new PlayerIdleState(playerCtrl, this));
        this.AddState(EPlayerState.Run, new PlayerRunState(playerCtrl, this));
        this.AddState(EPlayerState.Jump, new PlayerJumpState(playerCtrl, this));
        this.AddState(EPlayerState.WallSlide, new PlayerWallSlideState(playerCtrl, this));
        this.AddState(EPlayerState.Hurt, new PlayerHurtState(playerCtrl, this));
        this.AddState(EPlayerState.Dead, new PlayerDeadState(playerCtrl, this));

        //Defalt state
        this.ChangeState(EPlayerState.Idle);
    }

    public override void ExcuteState()
    {
        base.ExcuteState();
        this.UpdateParamAnimInAir();
        // Rigidbody2D rb = playerCtrl.rb;
        // float maxSpeed = playerCtrl.PlayerDataSO.moveSpeed;
        // rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
    }

    protected void UpdateParamAnimInAir()
    {
        if (!playerCtrl.PlayerGroundedCheck.IsGrounded)
        {
            playerCtrl.anim.SetFloat("yVelocity", playerCtrl.rb.velocity.y);
        }
    }

    public bool TryMoveState()
    {
        float moveInput = InputManager.Instance.MoveInput();
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            this.ChangeState(EPlayerState.Run);
            return true;
        }
        return false;
    }

    public bool TryJumpState()
    {
        bool jumpInput = InputManager.Instance.JumpInput();
        if (jumpInput)
        {
            this.ChangeState(EPlayerState.Jump);
            return true;
        }
        return false;
    }

    public bool TryIdleState()
    {
        float moveInput = InputManager.Instance.MoveInput();
        bool isGrounded = playerCtrl.PlayerGroundedCheck.IsGrounded;
        if (isGrounded && !(Mathf.Abs(moveInput) > 0.1f))
        {
            this.ChangeState(EPlayerState.Idle);
            return true;
        }
        return false;
    }

    public bool TryWallSlideState()
    {
        bool isWallSlidng = playerCtrl.PlayerWallSlidingCheck.IsWallSliding;
        if (isWallSlidng && playerCtrl.rb.velocity.y <= 0)
        {
            this.ChangeState(EPlayerState.WallSlide);
            return true;
        }
        return false;
    }
}
