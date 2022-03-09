using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Gravity : MonoBehaviour
{

    public float mass = 1000;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            collision.attachedRigidbody.AddForce((transform.position - collision.transform.position).normalized
                * (Time.fixedDeltaTime * mass * collision.attachedRigidbody.mass
                / Vector3.Distance(transform.position, collision.transform.position)));
        }
    }
}
