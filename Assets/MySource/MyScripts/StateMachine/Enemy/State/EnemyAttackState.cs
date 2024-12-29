using UnityEngine;

public class EnemyAttackState : BaseEnemyState
{
    private readonly EnemyController enemyCtrl;
    private readonly Blackboard<EEnemyBlackBoard> blackboard;
    private bool attacked = false;

    public EnemyAttackState(Blackboard<EEnemyBlackBoard> blackboard) : base()
    {
        this.blackboard = blackboard;
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public override void Enter()
    {
        this.attacked = false;
        this.enemyCtrl.anim.SetTrigger("attack");
    }

    public override void Excute()
    {
        var stateInfo = enemyCtrl.anim.GetCurrentAnimatorStateInfo(0);
        if (!this.attacked && stateInfo.IsTag("Attack") && stateInfo.normalizedTime >= stateInfo.length * 0.92f)
        {
            this.Attack();
            this.attacked = true;
        }
    }

    // public override EEnemyState CheckTransitions()
    // {
    //     if (isAttacking) return EEnemyState.None;

    //     return base.CheckTransitions();
    // }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)enemyCtrl.transform.position + new Vector2(0, -0.08f),
            enemyCtrl.FacingHandler.IsFacingRight ? Vector2.right : Vector2.left, this.enemyCtrl.EnemyData.attackRange);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable == null) return;

            damageable.Receiver(this.enemyCtrl.EnemyData.damage, enemyCtrl.transform.position);
        }
    }

    private float GetDirectionAttack()
    {
        Vector2 playerPosition = this.blackboard.GetValue<Transform>(EEnemyBlackBoard.PlayerTransform).position;
        Vector2 enemyPosition = this.enemyCtrl.transform.position;

        return playerPosition.x > enemyPosition.x ? 1 : -1;
    }
}
