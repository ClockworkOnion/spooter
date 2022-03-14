using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserCannon : AimedWeapon
{
    public float firingDistance = 10f;
    Transform playerTransform;
    // Start is called before the first frame update
    protected override void Start()
    {
        shipBody = GetComponentInParent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    protected override bool Refire()
    {
        if (playerTransform == null) return false;
        return (lastShot += Time.deltaTime) > refireRate && Vector3.Distance(playerTransform.position, transform.position) < firingDistance;
    }

    protected override bool Powered()
    {
        return true;
    }

    protected override float FiringAngle()
    {
        return Vector3.SignedAngle(playerTransform.position - transform.position, Vector3.up, Vector3.forward);
    }

    protected override bool CanFire(float angle)
    {
        angle = Vector3.SignedAngle(playerTransform.transform.position - transform.position, transform.up, Vector3.forward);
        for (int i = 0; i < firingAngle.Length; ++i)
        {
            if (angle > firingAngle[i] & angle < firingAngle[++i])
            {
                return true;
            }
        }
        return false;
    }

}
