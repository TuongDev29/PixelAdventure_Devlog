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
        this.enemyCtrl.FacingHandler.FlipTowards(this.GetDirectionAttack());
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

    private void Attack()
    {
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.useTriggers = false;
        filter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(enemyCtrl.gameObject.layer));

        Vector2 startPoint = (Vector2)enemyCtrl.transform.position + new Vector2(0, -0.08f);
        Vector2 direction = enemyCtrl.FacingHandler.IsFacingRight ? Vector2.right : Vector2.left;

        RaycastHit2D[] hits = Physics2D.RaycastAll(startPoint, direction, this.enemyCtrl.EnemyData.attackRange, filter2D.layerMask);

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable == null) return;
                damageable.Receiver(this.enemyCtrl.EnemyData.damage, enemyCtrl.transform.position);
            }
        }
    }

    private float GetDirectionAttack()
    {
        Vector2 playerPosition = this.blackboard.GetValue<Transform>(EEnemyBlackBoard.PlayerTransform).position;
        Vector2 enemyPosition = this.enemyCtrl.transform.position;

        return playerPosition.x > enemyPosition.x ? 1 : -1;
    }
}
