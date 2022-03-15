using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxShields : ShopItem
{
    [Range(0f, 100f)]
    public float[] shieldIncrease = new float[4];

    private DamageModel model;

    public override void ApplyUpgrade()
    {
        model = GameObject.Find("Player").GetComponent<DamageModel>();
        for (int i = 0; i < model.maxShield.Length; i++)
        {
            model.maxShield[i] += shieldIncrease[i];
        }
    }
}
