using UnityEngine;

public class BulletMovement : BaseMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 12f;
    [SerializeField] protected Vector2 direction = Vector2.right;

    public void SetDirectionFl(Vector2 direction)
    {
        this.direction = direction;
    }

    protected virtual void FixedUpdate()
    {
        transform.Translate(direction.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
