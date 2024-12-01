using System.Collections;
using System.Collections.Generic;
using DevLog;
using UnityEngine;

namespace DevLog
{
    public abstract class CharacterController : BaseMonoBehaviour
    {
        public Animator anim;
        public Collider2D Collider2D;

        protected override void LoadComponent()
        {
            base.LoadComponent();
            this.LoadAnimator();
            this.LoadCollider2D();
        }

        private void LoadAnimator()
        {
            if (this.anim != null) return;
            this.anim = transform.GetComponentInChildren<Animator>();
            Debug.LogWarning("LoadAnimator", gameObject);
        }

        private void LoadCollider2D()
        {
            if (this.Collider2D != null) return;
            this.Collider2D = transform.GetComponent<Collider2D>();
            Debug.LogWarning("LoadCollider2D", gameObject);
        }

    }

}