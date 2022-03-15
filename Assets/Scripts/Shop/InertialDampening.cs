using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertialDampening : ShopItem
{
    [Range(0f, 0.1f)]
    public float angularDrag;
    [Range(0f, 0.1f)]
    public float drag;
    private Rigidbody2D body;
   
    void Start()
    {
        body = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    public override void ApplyUpgrade()
    {
        body.angularDrag += angularDrag;
        body.drag += drag;
    }
}
