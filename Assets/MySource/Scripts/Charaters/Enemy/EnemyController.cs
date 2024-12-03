using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevLog;

namespace DevLog
{
    public class EnemyController : CharacterController
    {
        public Rigidbody2D rb;
        public EnemyDataSO EnemyData;
        public EnemyDamageable EnemyDamageable;
        public FacingHandler FacingHandler;
        protected EnemyStateMachine enemyStateMachine;

        protected virtual void Start()
        {
            this.FacingHandler = new FacingHandler(transform);
            this.enemyStateMachine = new EnemyStateMachine(this);
        }

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.LoadRigidbody2D();

            this.AutoLoadComponent<EnemyDamageable>(ref this.EnemyDamageable, GetComponent<EnemyDamageable>());
        }

        private void LoadRigidbody2D()
        {
            if (this.rb != null) return;
            this.rb = transform.GetComponent<Rigidbody2D>();
            this.rb.angularDrag = 10;
            Debug.LogWarning("LoadRigidbody2D", gameObject);
        }

        protected virtual void FixedUpdate()
        {
            this.enemyStateMachine?.ExcuteState();
        }
    }

}
