using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class DirectionalDamage : MonoBehaviour
{
    Collider2D coll;
    
    private void Start()
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
            model.Damage(GetTriggerDamage(trigger, coll)
                , DamageModel.AngleToDirection(Vector2.SignedAngle(transform.up
                , trigger.Distance(coll).normal)));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out DamageModel model)) { 
            ContactPoint2D contact = collision.GetContact(0);
            model.Damage(GetCollisionDamage(collision, contact)
                , DamageModel.AngleToDirection(Vector2.SignedAngle(transform.up
                , contact.point - (Vector2)transform.position)));
        }
    }
}
