using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : BaseMonoBehaviour
{
    [SerializeField] protected EnemyController enemyCtrl;
    [SerializeField] protected List<Vector2> patrolPoints;
    protected Blackboard<EEnemyBlackBoard> blackboard = new Blackboard<EEnemyBlackBoard>();
    protected StateMachine stateMachine;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<EnemyController>(ref this.enemyCtrl, gameObject);
    }

    protected virtual void Start()
    {
        this.InitBlackBoard();
        this.stateMachine = EnemyStateMachineFactory.Create(this.enemyCtrl.enemyCode, blackboard);
    }

    protected virtual void Update()
    {
        this.stateMachine.ExcuteState();
    }

    public void Initialize(List<Vector2> patrolPoints)
    {
        this.patrolPoints = patrolPoints;

        this.InitBlackBoard();
    }

    private void InitBlackBoard()
    {
        this.blackboard.SetValue(EEnemyBlackBoard.EnemyController, this.enemyCtrl);
        this.blackboard.SetValue(EEnemyBlackBoard.PatrolPoints, this.patrolPoints);
        this.blackboard.SetValue(EEnemyBlackBoard.EnemyTransform, transform);
        this.blackboard.SetValue(EEnemyBlackBoard.PlayerTransform, PlayerManager.Instance.CurrentPlayer.transform);
        this.blackboard.SetValue(EEnemyBlackBoard.PatrolIndex, 0);
        this.blackboard.SetValue(EEnemyBlackBoard.IsHungry, false);
    }

    // private void OnDrawGizmos()
    // {
    //     if (enemyCtrl == null) return;

    //     // Lấy vị trí ban đầu của Raycast.
    //     Vector2 startPoint = (Vector2)enemyCtrl.transform.position + new Vector2(0, -0.08f);

    //     // Lấy hướng của Raycast.
    //     Vector2 direction = enemyCtrl.FacingHandler.IsFacingRight ? Vector2.right : Vector2.left;

    //     // Tính điểm kết thúc của Raycast.
    //     Vector2 endPoint = startPoint + direction * enemyCtrl.EnemyData.attackRange;

    //     // Chọn màu cho Gizmos.
    //     Gizmos.color = Color.red;

    //     // Vẽ Raycast.
    //     // Gizmos.DrawLine(startPoint, endPoint);
    // }
}