using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public float mass = 100;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.attachedRigidbody.AddForce((transform.position - collision.transform.position).normalized 
            * mass * collision.attachedRigidbody.mass 
            / Vector3.Distance(transform.position, collision.transform.position));
    }
}
