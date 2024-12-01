
using System.Collections;
using UnityEngine;
using DevLog;

namespace DevLog
{
    public class PlayerHurtState : IState
    {
        private PlayerController playerCtrl;
        private PlayerStateMachine playerState;

        public PlayerHurtState(PlayerController _playerCtrl, PlayerStateMachine playerState)
        {
            this.playerCtrl = _playerCtrl;
            this.playerState = playerState;
        }

        public void Enter()
        {
            playerState.DelayTransition(0.2f);
            playerCtrl.anim.SetTrigger("hit");
            playerCtrl.gameObject.layer = 9; //Set layer is IgnoreHazards layer
        }

        public void Excute()
        {
            if (playerCtrl.rb.velocity.magnitude > 0.1f) return;

            playerCtrl.StartCoroutine(ChangeIdleStateRoutine());
        }

        public void Exit()
        {
            playerCtrl.StartCoroutine(ExitRoutine());
        }

        private IEnumerator ChangeIdleStateRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            playerState.ChangeState(EPlayerState.Idle);
        }

        private IEnumerator ExitRoutine()
        {
            playerCtrl.anim.SetTrigger("exit");

            yield return new WaitForSeconds(0.4f);
            playerCtrl.anim.ResetTrigger("exit");
            playerCtrl.gameObject.layer = 6; //Set layer is Player layer
        }
    }
}