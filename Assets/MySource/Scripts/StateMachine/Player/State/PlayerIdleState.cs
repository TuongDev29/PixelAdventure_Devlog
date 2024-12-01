using DevLog;
using UnityEngine;

namespace DevLog
{
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
        }

        public void Excute()
        {
            if (playerState.TryMoveState()) return;
            if (playerState.TryJumpState()) return;
            if (playerState.TryWallSlideState()) return;
        }

        public void Exit()
        {
        }
    }

}