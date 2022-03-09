using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : DirectionalDamage
{
    [Range(1f, 1000f)]
    public float damage = 10;

    public override float GetDamage(Collision2D collision, ContactPoint2D contact)
    {
        Destroy(gameObject);
        return damage;
    }
}
