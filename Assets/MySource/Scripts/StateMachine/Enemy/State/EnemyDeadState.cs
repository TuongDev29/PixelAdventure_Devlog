using UnityEngine;
using DevLog;

namespace DevLog
{
    public class EnemyDeadState : IState
    {
        private EnemyController enemy;
        private EnemyStateMachine state;

        public EnemyDeadState(EnemyController _enemy, EnemyStateMachine _state)
        {
            this.enemy = _enemy;
            this.state = _state;
        }

        public void Enter()
        {
            enemy.anim.SetBool("isDead", true);
        }

        public void Exit()
        {
            enemy.anim.SetBool("isDead", false);
        }

        public void Excute()
        {
        }
    }

}
