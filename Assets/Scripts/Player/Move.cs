using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody body;
    [Range(10f, 2000f)]
    public float angleSpeed = 1000;
    [Range(10f, 2000f)]
    public float dirSpeed = 1000;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddForce(transform.forward * dirSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical"));
        body.AddTorque(new Vector3(0, angleSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal"), 0));
    }
}
