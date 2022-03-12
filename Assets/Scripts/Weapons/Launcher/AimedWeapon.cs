using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AimedWeapon : Weapon
{
    [Range(-180f, 180f)]
    public float[] firingAngle;
    
    private Crosshair crosshair;

    protected override void Start()
    {
        base.Start();
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
    }

    protected override bool CanFire(float angle)
    {
        return true;
    }

    protected override float FiringAngle()
    {
        float angle = Vector2.SignedAngle(crosshair.transform.position - transform.position, Vector2.up);
        float angleToShip = Vector2.SignedAngle(crosshair.transform.position - transform.position, transform.up);
        if(firingAngle.Length == 2)
        {
            if(angleToShip < firingAngle[0])
            {
                return firingAngle[0];
            }
            if(angleToShip > firingAngle[1])
            {
                return firingAngle[1];
            }
            return angle;
        }
        for (int i = 1; i < firingAngle.Length - 1; i += 2)
        {
            if(angleToShip > firingAngle[i] && angleToShip < firingAngle[i + 1])
            {
                if(Mathf.DeltaAngle(firingAngle[i], angleToShip) < Mathf.DeltaAngle(angleToShip, firingAngle[i + 1]))
                {
                    return angle - Mathf.DeltaAngle(firingAngle[i], angleToShip);
                } else
                {
                    return angle + Mathf.DeltaAngle(angleToShip, firingAngle[i + 1]);
                }
            }
        }
        if(angleToShip < firingAngle[0] || angleToShip > firingAngle[firingAngle.Length - 1])
        {
            if (Mathf.DeltaAngle(firingAngle[firingAngle.Length - 1], angleToShip) < Mathf.DeltaAngle(angleToShip, firingAngle[0]))
            {
                return angle - Mathf.DeltaAngle(firingAngle[firingAngle.Length - 1], angleToShip);
            }
            else
            {
                return angle + Mathf.DeltaAngle(angleToShip, firingAngle[0]);
            }
        }
        return angle;
    }
}
