using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannon : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float maxFiringAngle = 90;
    public GameObject projectileType; // The gameobject/prefab that will be shot by this cannon
    private AudioSource audio;
    public AudioClip laserSound;

    private Crosshair crosshair;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        audio = GetComponent<AudioSource>();
        
    }

    void Update()
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
                // audio.PlayOneShot(laserSound);
            }
        }
    }
}
