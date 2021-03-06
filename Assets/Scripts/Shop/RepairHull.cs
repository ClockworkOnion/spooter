using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairHull : ShopItem
{
    [Range(0f, 100f)]
    public float amount;
    private DamageModel model;

    public override void ApplyUpgrade()
    {
        model = GameObject.Find("Player").GetComponent<DamageModel>();
        model.RepairHull(amount);
    }
}
