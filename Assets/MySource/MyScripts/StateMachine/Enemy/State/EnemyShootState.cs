using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyShootState : BaseEnemyState
{
    private readonly Blackboard<EEnemyBlackBoard> blackboard;
    private EnemyController enemyCtrl;
    private Vector2 bulletPositionOffset = new Vector2(0, -0.08f);
    private bool isShooted = false;
    private Vector2 directionFLy;

    public EnemyShootState(Blackboard<EEnemyBlackBoard> blackboard)
    {
        this.blackboard = blackboard;
        this.enemyCtrl = blackboard.GetValue<EnemyController>(EEnemyBlackBoard.EnemyController);
    }

    public override void Enter()
    {
        this.enemyCtrl.anim.SetTrigger("attack");
        this.isShooted = false;
        this.directionFLy = this.GetDirectionToPlayer();
        this.enemyCtrl.FacingHandler.FlipTowards(directionFLy);
    }

    public override void Excute()
    {
        var stateInfo = enemyCtrl.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsTag("Attack") && stateInfo.normalizedTime >= stateInfo.length * 0.70f)
        {
            if (isShooted) return;
            this.Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 bulletPosition = (Vector2)enemyCtrl.transform.position + this.bulletPositionOffset;
        BulletSpawner.Instance.SpawnBullet(EBulletCode.TrunkBullet, bulletPosition, directionFLy);
        this.isShooted = true;
    }

    private Vector2 GetDirectionToPlayer()
    {
        Vector2 playerPos = this.blackboard.GetValue<Transform>(EEnemyBlackBoard.PlayerTransform).position;

        return playerPos.x > enemyCtrl.transform.position.x ? Vector2.right : Vector2.left;
    }
}