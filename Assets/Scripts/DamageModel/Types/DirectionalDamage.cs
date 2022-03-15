using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class DirectionalDamage : MonoBehaviour
{
    Collider2D coll;
    
    protected virtual void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    
    public virtual float GetCollisionDamage(Collision2D collision, ContactPoint2D contact)
    {
        return 0;
    }

    public virtual float GetTriggerDamage(Collider2D trigger, Collider2D triggered)
    {
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.TryGetComponent(out DamageModel model))
        {
      
            DamageDirection dd = DamageModel.AngleToDirection(Vector2.SignedAngle(trigger.transform.up
                , trigger.Distance(coll).normal));
            model.Damage(GetTriggerDamage(trigger, coll), dd);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out DamageModel model)) { 
            ContactPoint2D contact = collision.GetContact(0);
            model.Damage(GetCollisionDamage(collision, contact)
                , DamageModel.AngleToDirection(Vector2.SignedAngle(collision.collider.transform.up
                , contact.point - (Vector2)collision.collider.transform.position)));
        }
    }
}
