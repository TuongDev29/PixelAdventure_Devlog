using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevLog;

namespace DevLog
{
    public class TrampolimeHandler : MainMonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private float upwordForce = 16;
        [SerializeField] private Animator anim;

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.LoadTrigger2D();
            this.LoadAnimator();
        }

        private void LoadAnimator()
        {
            if (this.anim != null) return;
            this.anim = transform.GetComponentInChildren<Animator>();
            Debug.LogWarning(transform.name + "LoadAnimator", gameObject);
        }

        private void LoadTrigger2D()
        {
            if (this._boxCollider2D != null) return;
            this._boxCollider2D = transform.GetComponent<BoxCollider2D>();
            this._boxCollider2D.isTrigger = true;

            Debug.LogWarning(transform.name + "LoadTrigger2D", gameObject);
        }

        //This is layer only Interaction with player 
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.anim.SetTrigger("propel");
                PlayerController playerCtrl = other.GetComponent<PlayerController>();

                playerCtrl.rb.velocity = Vector2.zero;
                playerCtrl.rb.AddForce(Vector2.up * this.upwordForce, ForceMode2D.Impulse);
            }
        }
    }
}