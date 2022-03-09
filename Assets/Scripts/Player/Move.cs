using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [Range(1f, 10f)]
    public float angleSpeed;
    [Range(1f, 100f)]
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
        body.AddForce(dirSpeed * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical") * transform.up);
        body.AddTorque(angleSpeed * Time.fixedDeltaTime * -Input.GetAxisRaw("Horizontal"));
    }
}
