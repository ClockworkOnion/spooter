using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageModel : DamageModel
{
    public GameObject explosion;

    public override void OnHullDestroyed()
    {
        Instantiate(explosion, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
