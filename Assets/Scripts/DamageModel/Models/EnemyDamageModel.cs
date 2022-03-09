using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageModel : DamageModel
{
    public override void OnHullDestroyed()
    {
        Destroy(gameObject);
    }
}
