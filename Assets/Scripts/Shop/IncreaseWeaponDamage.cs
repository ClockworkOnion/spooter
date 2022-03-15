using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseWeaponDamage : ShopItem
{
    public float damage;
    private LaserDamage weapon;

    public override void ApplyUpgrade()
    {
        weapon = GameObject.Find("Player").GetComponent<LaserCannon>().projectileType.GetComponent<LaserDamage>();
        weapon.damage += damage;
    }
}
