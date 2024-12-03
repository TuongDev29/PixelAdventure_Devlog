using UnityEngine;

namespace DevLog
{
    public class EnemyIdleState : IState
    {
        private EnemyController enemy;
        private EnemyStateMachine enemyState;

        public EnemyIdleState(EnemyController _enemy, EnemyStateMachine _state)
        {
            this.enemy = _enemy;
            this.enemyState = _state;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Excute()
        {
            if (enemyState.TryDeadState()) return;
        }
    }

}
