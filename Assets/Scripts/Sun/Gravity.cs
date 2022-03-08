using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public float mass = 10000000;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = Mathf.Sqrt(.1f * mass);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.attachedRigidbody.AddForce((transform.position - collision.transform.position).normalized
                * mass * collision.attachedRigidbody.mass 
                / Mathf.Pow(Vector3.Distance(transform.position, collision.transform.position), 2));
        }
    }
}
