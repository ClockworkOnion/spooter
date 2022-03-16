using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseEnergy : ShopItem
{
    [Range(0f, 100f)]
    public float amount;

    public override void ApplyUpgrade()
    {
        ShipSystems sys = GameObject.Find("Player").GetComponent<ShipSystems>();
        sys.maxEnergyPool += amount;
    }
}
