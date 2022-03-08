using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public float strength = 10;

    private void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.AddForce(strength * (transform.position - other.transform.position).normalized / Mathf.Pow(Vector3.Distance(transform.position, other.transform.position), 2));
    }
}
