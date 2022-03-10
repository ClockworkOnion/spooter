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
    public float thrusterCost = 5f;
    public float turningCost = 2f;
    Rigidbody2D body;
    ShipSystems systems;
    [SerializeField]
    private float verticalThrust;
    [SerializeField]
    private float horizontalThrust;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        systems = GetComponent<ShipSystems>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && systems.useEnergy(thrusterCost*Time.deltaTime))
        {
            verticalThrust = Input.GetAxisRaw("Vertical");
        } else { verticalThrust = 0f;  }

        if (Input.GetAxisRaw("Horizontal") != 0 && systems.useEnergy(turningCost*Time.deltaTime))
        {
            horizontalThrust = Input.GetAxisRaw("Horizontal");
        } else { horizontalThrust = 0f;  }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        body.AddForce(dirSpeed * Time.fixedDeltaTime * verticalThrust * transform.up);
        body.AddTorque(angleSpeed * Time.fixedDeltaTime * -horizontalThrust);
    }
}
