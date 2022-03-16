using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDamage : DirectionalDamage
{
    public float damage;
    public float armTime;
    private float deployTime;

    public GameObject explosion;

    protected override void Start()
    {
        deployTime = Time.time;
        base.Start();
    }

    public override float GetCollisionDamage(Collision2D collision, ContactPoint2D contact)
    {
        if (Time.time - deployTime > armTime)
        {   
            Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
            Destroy(gameObject);
            return damage;
        }
        return 0;
    }

}
