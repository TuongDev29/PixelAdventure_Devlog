using UnityEngine;
using DevLog;

namespace DevLog
{
    public class EnemyStateMachine : BaseStateMachine
    {
        public enum EEnemyState
        {
            Idle,
            Patrol,
            Chase,
            Dead
        }

        protected EnemyController enemyCtrl;

        public EnemyStateMachine(MonoBehaviour monoBehaviour) : base(monoBehaviour)
        {
            this.enemyCtrl = (monoBehaviour as EnemyController);
        }

        public virtual bool TryIdleState()
        {
            if (!this.ContainState(EEnemyState.Idle)) return false;

            this.ChangeState(EEnemyState.Idle);
            return true;
        }

        public virtual bool TryDeadState()
        {
            Debug.Log(!this.ContainState(EEnemyState.Dead) + " / " + this.enemyCtrl.EnemyDamageable.IsDead);
            if (!this.ContainState(EEnemyState.Dead)) return false;

            if (this.enemyCtrl.EnemyDamageable.IsDead)
            {
                this.ChangeState(EEnemyState.Dead);
                return true;
            }
            return false;
        }

        protected override void InitializeState()
        {
            this.AddState(EEnemyState.Idle, new EnemyIdleState(enemyCtrl, this));
            this.AddState(EEnemyState.Dead, new EnemyDeadState(enemyCtrl, this));

            this.ChangeState(EEnemyState.Idle);
        }
    }

}
