using UnityEngine;
using DevLog;

namespace DevLog
{
    public abstract class WallSlidingCheck<T> : WalledCheck<T> where T : class, ICheckCollisionLeft, ICheckCollisionRight
    {
        private bool previousWallSlidingState;
        [SerializeField] private bool _isWallSliding;
        public bool IsWallSliding => _isWallSliding;

        protected WallSlidingCheck(MonoBehaviour monoBehaviour) : base(monoBehaviour) { }

        protected override void OnChecking()
        {
            base.OnChecking();
            this._isWallSliding = CheckWallSliding();

            if (this.IsWalled)
            {
                if (!this.previousWallSlidingState)
                    OnWallSlidingEnter();

                this.OnWallSliding();
            }
            else
            {
                if (this.previousWallSlidingState)
                    this.OnWallSlidingExit();
            }
            this.previousWallSlidingState = this.IsWalled;
        }

        protected abstract bool CheckWallSliding();

        protected virtual void OnWallSlidingEnter() { }
        protected virtual void OnWallSlidingExit() { }
        protected virtual void OnWallSliding() { }
    }

}