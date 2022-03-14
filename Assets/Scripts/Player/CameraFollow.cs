using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Rigidbody2D plrRB;
    public bool followCursor = false;
    private Vector3 vel = Vector3.zero;
    private Transform crosshairTransform;
    public Vector3 offset = new Vector3(0, 0, -100);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        plrRB = player.GetComponent<Rigidbody2D>();
        crosshairTransform = GameObject.Find("Crosshair").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;
        if (followCursor)
        {
            Vector3 playerToCrosshair =  crosshairTransform.position - player.transform.position;
            Vector3 goalPosition = player.transform.position + offset + playerToCrosshair*0.25f + (Vector3)plrRB.velocity * 0.15f;
            transform.position = Vector3.SmoothDamp(transform.position, goalPosition, ref vel, 0.1f);
        } else
        {
            transform.position = player.transform.position + offset;
        }
    }
}
