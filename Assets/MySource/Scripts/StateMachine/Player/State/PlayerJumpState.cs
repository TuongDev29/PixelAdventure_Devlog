using UnityEngine;
using DevLog;

namespace DevLog
{
    public class PlayerJumpState : IState, IObserveGrounded, IObserveWalled
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
            this.playerCtrl.PlayerWallSlidingCheck.RegisterObserveWalled(this);
        }

        public void Enter()
        {

            if (remainJump >= playerCtrl.PlayerDataSO.maxJump) return;
            if (remainJump > 0)
                playerCtrl.anim.SetTrigger("doubleJump");

            playerCtrl.rb.velocity = Vector2.zero;
            playerCtrl.rb.AddForce(Vector2.up * playerCtrl.PlayerDataSO.jumpForce, ForceMode2D.Impulse);
            remainJump++;
        }

        public void Excute()
        {
            if (playerState.TryJumpState()) return;
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
            playerCtrl.anim.SetBool("isGrounded", false);
        }

        public void OnWalledEnter()
        {
            this.ResetRemainJump();
        }

        public void OnWalledExit()
        {

        }
    }

}