using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : BaseMonoBehaviour
{
    public Animator anim;
    public Collider2D Collider2D;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchChildComponent(ref this.anim, gameObject);
        UntilityHelper.AutoFetchComponent(ref this.Collider2D, gameObject);
    }
}
