using UnityEngine;

public class FacingHandler
{
    private int initialOrientationX = 1;
    protected Transform transform;
    protected Vector2 facingDirection = Vector2.one;
    public bool IsFacingRight => facingDirection.x > 0;

    public FacingHandler(Transform targetTransform, int initialOrientationX = default)
    {
        if (initialOrientationX != default)
        {
            this.initialOrientationX = initialOrientationX;
        }

        this.transform = targetTransform;
    }

    public bool ToggleFlip()
    {
        this.facingDirection.x *= -1;
        this.ApplyFlip();

        return this.facingDirection.x > 0;
    }

    public void FlipTowards(bool faceRight)
    {
        if ((faceRight && this.facingDirection.x > 0) || (!faceRight && this.facingDirection.x < 0)) return;

        this.ToggleFlip();
    }

    public void FlipTowards(Vector2 direction)
    {
        if (direction.x == 0) return;

        bool faceRight = direction.x > 0;
        this.FlipTowards(faceRight);
    }

    public void FlipTowards(float direction)
    {
        if (direction == 0) return;

        bool faceRight = direction > 0;
        this.FlipTowards(faceRight);
    }

    protected void ApplyFlip()
    {
        Vector2 scale = Vector2.one;
        scale.x = this.facingDirection.x * this.initialOrientationX;
        transform.localScale = scale;
    }
}