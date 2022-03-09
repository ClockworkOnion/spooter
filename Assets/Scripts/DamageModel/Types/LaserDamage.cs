using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : DirectionalDamage
{
    [Range(1f, 1000f)]
    public float damage = 10;
    Collider2D ship;
    bool leftShip = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(ship == null)
        {
            ship = collision;
        } else if(ship != collision)
        {
            ship = null;
            leftShip = true;
        }
        
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
