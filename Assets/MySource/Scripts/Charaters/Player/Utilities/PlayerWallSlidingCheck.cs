using System;
using UnityEngine;
using DevLog;

namespace DevLog
{
    [Serializable]
    public class PlayerWallSlidingCheck : WallSlidingCheck<PlayerWallSlidingCheck>, ICheckCollisionLeft, ICheckCollisionRight
    {
        protected PlayerController playerCtrl;
        protected Vector2 sizeCollider;
        protected Vector2 offsetColldier;
        [SerializeField] protected float distanceChecking = 0.36f;

        public PlayerWallSlidingCheck(MonoBehaviour monoBehaviour) : base(monoBehaviour)
        {
            this.playerCtrl = (monoBehaviour as PlayerController);
            this.sizeCollider = (playerCtrl.Collider2D as CapsuleCollider2D).size;
            this.offsetColldier = (playerCtrl.Collider2D as CapsuleCollider2D).offset;

            GizmosDrawer.Instance.AddDrawAction(() =>
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(playerCtrl.transform.position, Vector2.right * distanceChecking);
            });
        }

        public bool CheckCollisionLeft()
        {
            RaycastHit2D hit = Physics2D.CapsuleCast(playerCtrl.transform.position, sizeCollider,
                    CapsuleDirection2D.Vertical, 0, Vector2.left, distanceChecking - (sizeCollider.x / 2));

            return hit.collider != null && !hit.collider.isTrigger;
        }

        public bool CheckCollisionRight()
        {
            RaycastHit2D hit = Physics2D.CapsuleCast(playerCtrl.transform.position, sizeCollider,
                    CapsuleDirection2D.Vertical, 0, Vector2.right, distanceChecking - (sizeCollider.x / 2));

            return hit.collider != null && !hit.collider.isTrigger;
        }

        protected override bool CheckWallSliding()
        {
            return this.IsWalled && !playerCtrl.PlayerGroundedCheck.IsGrounded;
        }
    }

}