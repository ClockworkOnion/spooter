using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannon : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float maxFiringAngle = 90;
    public GameObject projectileType; // The gameobject/prefab that will be shot by this cannon

    private Crosshair crosshair;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 directionToCrosshair = (crosshair.transform.position - transform.position).normalized;
            Quaternion rotationToCrosshair = Quaternion.LookRotation(directionToCrosshair);
            float firingAngle = Vector3.Angle(directionToCrosshair, transform.up);
            if (firingAngle < maxFiringAngle)
            {
                LaserProjectile laser = Instantiate(projectileType, transform.position, rotationToCrosshair).GetComponent<LaserProjectile>();
            }
        }
    }
}
