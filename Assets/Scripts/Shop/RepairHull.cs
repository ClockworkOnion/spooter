using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairHull : ShopItem
{
    [Range(0f, 100f)]
    public float amount;
    private DamageModel model;

    private void Start()
    {
        model = GameObject.Find("Player").GetComponent<DamageModel>();
        text = $"Gain {amount} hp";
    }

    public override void ApplyUpgrade()
    {
        model.RepairHull(amount);
    }
}
