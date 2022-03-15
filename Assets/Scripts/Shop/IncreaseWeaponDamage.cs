using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseWeaponDamage : ShopItem
{
    public float damage;
    private LaserDamage weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Player").GetComponent<LaserCannon>().projectileType.GetComponent<LaserDamage>();
    }

    public override void ApplyUpgrade()
    {
        weapon.damage += damage;
    }
}
