using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [Range(1000f, 4000f)]
    public float angleSpeed;
    [Range(10f, 2000f)]
    public float dirSpeed;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddForce(transform.up * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical") * dirSpeed);
        body.AddTorque(angleSpeed * Time.fixedDeltaTime * -Input.GetAxisRaw("Horizontal"));
    }
}
