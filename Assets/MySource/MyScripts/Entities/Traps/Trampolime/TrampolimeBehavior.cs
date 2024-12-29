using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolimeBehavior : BaseMonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Animator _anim;
    [SerializeField] private float bounceForce = 16;
    [SerializeField] private Vector2 bounceDirection = Vector2.up;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollision2D();
        this.LoadAnimator();
    }

    private void LoadAnimator()
    {
        if (this._anim != null) return;
        this._anim = transform.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + "LoadAnimator", gameObject);
    }

    private void LoadCollision2D()
    {
        if (this._boxCollider2D != null) return;
        this._boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + "LoadCollision2D", gameObject);
    }

    //This is layer only Interaction with player 
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        Transform playerTransform = other.transform;
        if (playerTransform.CompareTag("Player"))
        {
            PlayerController playerCtrl = playerTransform.GetComponent<PlayerController>();

            playerCtrl.rb.velocity = Vector2.zero;
            playerCtrl.rb.AddForce(this.bounceDirection * this.bounceForce, ForceMode2D.Impulse);

            //Update animation of Trampoline is propel
            this._anim.SetTrigger("propel");
        }
    }
}
