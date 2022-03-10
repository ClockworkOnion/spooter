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
        angle = Vector3.SignedAngle(crosshair.transform.position - transform.position, transform.up, Vector3.forward);
        for (int i = 0; i < firingAngle.Length; ++i)
        {
            if (angle > firingAngle[i] & angle < firingAngle[++i])
            {
                return true;
            }
        }
        return false;
    }

    protected override float FiringAngle()
    {
        return Vector3.SignedAngle(crosshair.transform.position - transform.position, Vector3.up, Vector3.forward);
    }
}
