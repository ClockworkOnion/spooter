using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : DirectionalDamage
{
    [Range(1f, 1000f)]
    public float damage = 10;
    bool leftShip = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        leftShip = true;
    }

    public override float GetTriggerDamage(Collider2D collider, Collider2D other)
    {
        if (leftShip)
        {
            Destroy(gameObject);
            return damage;
        }
        return 0;
    }
}
