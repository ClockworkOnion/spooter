using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionalWeapon : Weapon
{
    [Range(-180f, 180f)]
    public float firingAngle;

    protected override float FiringAngle()
    {
        return ((firingAngle + (Vector2.SignedAngle(transform.up, Vector2.up) + 180)) % 360) - 180;
    }

    protected override bool CanFire(float angle)
    {
        return true;
    }
}
