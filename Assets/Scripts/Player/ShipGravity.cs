using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGravity : MonoBehaviour
{
    public float mass = 1000;
    Collider2D ship;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponentsInParent<Collider2D>()[1];
    }

    private  void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.attachedRigidbody != null && collision.Distance(ship).distance > 0)
        {
            collision.attachedRigidbody.AddForce((transform.position - collision.transform.position).normalized
            * (Time.fixedDeltaTime * mass * collision.attachedRigidbody.mass
            / Vector3.Distance(transform.position, collision.transform.position)));
        }
    }
}
