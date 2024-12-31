using System;
using UnityEngine;

[Serializable]
public class PlayerWallSlidingCheck : WallSlidingCheck<PlayerWallSlidingCheck>, ICheckCollisionLeft, ICheckCollisionRight
{
    protected PlayerController playerCtrl;
    protected Vector2 sizeCollider;
    protected Vector2 offsetColldier;
    [SerializeField] protected float distanceChecking = 0.04f;

    public PlayerWallSlidingCheck(PlayerController playerController) : base()
    {
        this.playerCtrl = playerController;
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
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.useTriggers = false;
        filter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(playerCtrl.gameObject.layer));

        RaycastHit2D hit = Physics2D.CapsuleCast(playerCtrl.transform.position, sizeCollider,
                CapsuleDirection2D.Vertical, 0, Vector2.left, distanceChecking, filter2D.layerMask);

        return hit.collider != null;
        // return hit.collider != null && !hit.collider.isTrigger;
    }

    public bool CheckCollisionRight()
    {
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.useTriggers = false;
        filter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(playerCtrl.gameObject.layer));

        RaycastHit2D hit = Physics2D.CapsuleCast(playerCtrl.transform.position, sizeCollider,
                CapsuleDirection2D.Vertical, 0, Vector2.right, distanceChecking, filter2D.layerMask);

        return hit.collider != null;
    }

    protected override bool CheckWallSliding()
    {
        return this.IsWalled && !playerCtrl.PlayerGroundedCheck.IsGrounded;
    }
}
