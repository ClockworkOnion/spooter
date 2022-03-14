using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageModel : DamageModel
{
    public GameObject explosion;

    [Range(1, 100)]
    public uint gainedScore;

    public override void OnHullDestroyed()
    {
        GameManager.GetManager().AddScore(gainedScore);
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
        Destroy(gameObject);
    }
}
