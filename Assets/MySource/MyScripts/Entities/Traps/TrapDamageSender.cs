using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageSender : DamageSender
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Receiver(this.damage, transform.position);
            }
        }
    }
}
