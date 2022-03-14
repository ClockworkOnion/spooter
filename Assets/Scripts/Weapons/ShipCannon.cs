using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannon : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float maxFiringAngle = 90;

    [Tooltip("Gameobject/Prefab that will be shot by this cannon")]
    public GameObject projectileType; 

    private Crosshair crosshair;
    private Rigidbody2D rb;
    [SerializeField]
    private bool onEnemy;

    private Transform playerTransform;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponentInParent<Rigidbody2D>();
        if (rb.gameObject.tag.Equals("Enemy")) onEnemy = true;

    }

    void Update()
    {
        if (!onEnemy)
        {
            HandlePlayerShots();
            return;
        }

    }

    void HandlePlayerShots()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 directionToCrosshair = (crosshair.transform.position - transform.position).normalized;
            float firingAngle = Vector3.Angle(directionToCrosshair, transform.up);
            Quaternion rotationToCrosshair = Quaternion.Euler(0, 0, 
                -Vector3.SignedAngle(directionToCrosshair, Vector3.up, Vector3.forward));
            if (firingAngle < maxFiringAngle)
            {
                LaserProjectile laser = Instantiate(projectileType, transform.position, rotationToCrosshair).GetComponent<LaserProjectile>();
            }
        }
    }
}
