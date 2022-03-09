using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashDamage : DirectionalDamage
{
    public override float GetCollisionDamage(Collision2D collision, ContactPoint2D contact)
    {
        return contact.relativeVelocity.magnitude * contact.otherRigidbody.mass * contact.rigidbody.mass;
    }
}
