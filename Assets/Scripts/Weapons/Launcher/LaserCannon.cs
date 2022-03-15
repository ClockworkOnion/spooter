using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : AimedWeapon
{
    [Range(0f, 100f)]
    public float drain;
    [Range(0f, 1000f)]
    public float damage;
    protected override void Start()
    {
        projectileType.GetComponent<LaserDamage>().damage = damage;
        base.Start();
    }

    protected override bool Powered()
    {
        return systems.useEnergy(drain);
    }
}
