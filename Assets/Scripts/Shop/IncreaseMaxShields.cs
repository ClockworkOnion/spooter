using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxShields : ShopItem
{
    [Range(0f, 100f)]
    public float[] shieldIncrease = new float[4];

    private DamageModel model;
    void Start()
    {
        model = GameObject.Find("Player").GetComponent<DamageModel>();
    }

    public override void ApplyUpgrade()
    {
        for (int i = 0; i < model.maxShield.Length; i++)
        {
            model.maxShield[i] += shieldIncrease[i];
        }
    }
}
