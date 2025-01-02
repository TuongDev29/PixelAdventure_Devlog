
using System.Collections;
using UnityEngine;

public class PlayerHurtState : IState
{
    private readonly PlayerController playerCtrl;
    private readonly PlayerStateMachine playerState;
    private readonly float invincibleDuration = 3.6f;
    private float invincibleTimer = 0;
    private LimitedInvoker limitedInvoker;

    public PlayerHurtState(PlayerController _playerCtrl, PlayerStateMachine playerState)
    {
        this.playerCtrl = _playerCtrl;
        this.playerState = playerState;
        this.limitedInvoker = new LimitedInvoker(ChangeIdleStateFunc);
    }

    public void Enter()
    {
        playerState.DelayTransition(0.2f);
        playerCtrl.anim.SetTrigger("hit");
        playerCtrl.gameObject.layer = 9; //Set layer is IgnoreHazards layer

        // Set InvincibleTime
        this.invincibleTimer = this.invincibleDuration;
        this.limitedInvoker.AddInvokeNumber(1);
    }

    public void Excute()
    {
        this.ReduceSpeed(0.94f);
        this.invincibleTimer -= Time.deltaTime;

        if (playerCtrl.rb.velocity.magnitude > 0.1f) return;

        this.limitedInvoker.Invoke();
    }

    public void Exit()
    {
        CoroutineManager.Instance.StartManagedCoroutine(ExitRoutine());
    }

    private void ChangeIdleStateFunc()
    {
        CoroutineManager.Instance.StartManagedCoroutine(ChangeIdleStateRoutine());
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

        yield return new WaitForSeconds(0.6f);
        playerCtrl.anim.ResetTrigger("exit");
        playerCtrl.gameObject.layer = 6; //Set layer is Player layer
    }
}