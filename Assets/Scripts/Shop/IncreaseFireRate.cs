using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFireRate : ShopItem
{

    public float increasePercentage;
    public Weapon weapon;

    public override void ApplyUpgrade()
    {
        weapon = GameObject.Find("Player").GetComponent<MissleLauncher>();
        weapon.refireRate = 1 / ((1 / weapon.refireRate) * (1 + (increasePercentage/100)));
    }
}
