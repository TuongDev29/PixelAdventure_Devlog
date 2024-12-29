
using System.Collections;
using UnityEngine;

public class PlayerHurtState : IState
{
    private readonly PlayerController playerCtrl;
    private readonly PlayerStateMachine playerState;
    private readonly float invincibleTime = 2.8f;
    private float timer = 0;

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

        // Set InvincibleTime
        this.timer = this.invincibleTime;
    }

    public void Excute()
    {
        this.ReduceSpeed(0.94f);
        this.timer -= Time.deltaTime;
        if (this.timer > 0 && playerCtrl.rb.velocity.magnitude > 0.1f) return;

        CoroutineManager.Instance.StartManagedCoroutine(ChangeIdleStateRoutine());
    }

    public void Exit()
    {
        CoroutineManager.Instance.StartManagedCoroutine(ExitRoutine());
    }

    private void ReduceSpeed(float factor)
    {
        // Reduce speed
        Vector2 velocity = playerCtrl.rb.velocity;
        velocity.x *= factor;
        playerCtrl.rb.velocity = velocity;
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


//  private IEnumerator HurtTransitionAndIgnoreHazardsChecking()
//     {
//         playerCtrl.PlayerState.DisableTransition();
//         playerCtrl.gameObject.layer = 9; //Set layer is IgnoreHazards layer

//         yield return new WaitForSeconds(0.2f);
//         while (!playerCtrl.PlayerState.CanTransition &&
//             playerCtrl.rb.velocity.magnitude > 0.1f)
//         {
//             if (playerCtrl.PlayerState.CompareState(EPlayerState.Dead)) yield break;

//             yield return new WaitForSeconds(0.2f);
//         }

//         playerCtrl.PlayerState.EnableTransition();

//         yield return new WaitForSeconds(0.4f);
//         playerCtrl.gameObject.layer = 6; //Set layer is Player layer
//     }