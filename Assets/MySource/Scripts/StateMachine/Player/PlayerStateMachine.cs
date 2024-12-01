using System.Collections;
using DevLog;
using UnityEngine;

namespace DevLog
{
    public class PlayerStateMachine : BaseStateMachine
    {
        private DevLog.PlayerController playerCtrl;

        public PlayerStateMachine(MonoBehaviour monoBehaviour) : base(monoBehaviour)
        {
            this.playerCtrl = (monoBehaviour as PlayerController);
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

            this.UpdateAnimator();
        }

        protected void UpdateAnimator()
        {
            playerCtrl.anim.SetFloat("yVelocity", playerCtrl.rb.velocity.y);
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
}