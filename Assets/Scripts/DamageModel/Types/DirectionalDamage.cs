using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class DirectionalDamage : MonoBehaviour
{
    public abstract float GetDamage(Collision2D collision, ContactPoint2D contact);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out DamageModel model)) { 
            ContactPoint2D contact = collision.GetContact(0);
            model.Damage(GetDamage(collision, contact)
                , DamageModel.AngleToDirection(Vector2.SignedAngle(transform.up
                , contact.point - (Vector2)transform.position)));
        }
    }
}
