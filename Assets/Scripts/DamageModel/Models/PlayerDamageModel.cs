using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageModel : DamageModel
{
    [Tooltip("The particle system that should be spawned when the player dies")]
    public GameObject explosionPrefab;
    public override void OnHullDestroyed()
    {
        Destroy(Instantiate(explosionPrefab, transform.position, transform.rotation), 2);
        Destroy(gameObject);
        GameManager.GetManager().GameOver();
    }
}
