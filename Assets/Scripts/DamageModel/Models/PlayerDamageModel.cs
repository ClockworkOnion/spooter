using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageModel : DamageModel
{
    public override void OnHullDestroyed()
    {
        GameManager.GetManager().GameOver();
    }
}
