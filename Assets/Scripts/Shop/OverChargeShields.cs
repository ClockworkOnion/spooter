using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverChargeShields : ShopItem
{
    [Range(0f, 100f)]
    public float amount;
    public override void ApplyUpgrade()
    {
        DamageModel model = GameObject.Find("Player").GetComponent<DamageModel>();
        model.OverCharge(amount);
    }
}
