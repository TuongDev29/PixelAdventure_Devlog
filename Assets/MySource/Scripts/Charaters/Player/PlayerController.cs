using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevLog;

namespace DevLog
{
    public class PlayerController : CharacterController
    {
        [Header("PlayerController")]
        public PlayerDataSO PlayerDataSO;
        public Rigidbody2D rb;

        public BaseStateMachine PlayerState;
        public PlayerGroundedCheck PlayerGroundedCheck;
        public PlayerWallSlidingCheck PlayerWallSlidingCheck;
        public FacingHandler FacingHandler;

        protected virtual void Start()
        {
            this.PlayerGroundedCheck = new PlayerGroundedCheck(this);
            this.PlayerWallSlidingCheck = new PlayerWallSlidingCheck(this);
            this.FacingHandler = new FacingHandler(transform);

            this.PlayerState = new PlayerStateMachine(this);
        }

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.LoadRigidbody2D();
        }

        private void LoadRigidbody2D()
        {
            if (this.rb != null) return;
            this.rb = GetComponent<Rigidbody2D>();
            Debug.LogWarning("LoadRigidbody2D", gameObject);
        }

        protected virtual void Update()
        {
            this.PlayerState?.ExcuteState();

            if (Input.GetKeyDown(KeyCode.F)) PlayerState.ChangeState(EPlayerState.Hurt);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (this.PlayerState.stateAction is ITrigger2DState stateTriggerHandle)
            {
                stateTriggerHandle.OnTriggerEnter2D(other);
            }
        }
    }

}