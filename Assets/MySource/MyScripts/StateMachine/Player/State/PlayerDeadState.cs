using System.Collections;
using UnityEngine;

public class PlayerDeadState : IState
{
    private PlayerController playerCtrl;

    public PlayerDeadState(PlayerController _playerCtrl, PlayerStateMachine playerState)
    {
        this.playerCtrl = _playerCtrl;
    }

    public void Enter()
    {
        playerCtrl.anim.SetBool("isDead", true);
    }

    public void Excute()
    {
    }

    public void Exit()
    {
        playerCtrl.anim.SetBool("isDead", false);
    }
}