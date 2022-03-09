using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float flightSpeed = 3.0f;
    private float lifeSpan = 10.0f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * flightSpeed;

        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0) { GameObject.Destroy(this.gameObject); }
    }
}
