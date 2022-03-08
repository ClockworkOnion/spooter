using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Range(10f, 2000f)]
    public float angleSpeed = 1000;
    [Range(10f, 2000f)]
    public float dirSpeed = 1000;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddForce(transform.up * Time.deltaTime * Input.GetAxisRaw("Vertical") * dirSpeed);
        body.AddTorque(angleSpeed * Time.deltaTime * -Input.GetAxisRaw("Horizontal"));
    }
}
