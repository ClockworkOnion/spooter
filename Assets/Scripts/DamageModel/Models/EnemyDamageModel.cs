using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageModel : DamageModel
{
    public GameObject explosion;

    public override void OnHullDestroyed()
    {
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
        Destroy(gameObject);
    }
}
