using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AimedWeapon : MonoBehaviour, IWeapon
{
    [Range(-180f, 180f)]
    public float[] firingAngle;
    [Range(0f, 20f)]
    public float lifeTime;
    [Range(0, 1)]
    public int weapon;
    public AudioClip laserSound;
    public GameObject projectileType; // The gameobject/prefab that will be shot by this cannon
    
    private AudioSource audioSource;
    private Crosshair crosshair;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(weapon))
        {
            Vector3 directionToCrosshair = (crosshair.transform.position - transform.position).normalized;
            float firingAngle = Vector3.SignedAngle(directionToCrosshair, transform.up, Vector3.forward);
            Quaternion rotationToCrosshair = Quaternion.Euler(0, 0,
                -Vector3.SignedAngle(directionToCrosshair, Vector3.up, Vector3.forward));
            if (CanFire(firingAngle) && CanFire())
            {
                Destroy(Instantiate(projectileType, transform.position, rotationToCrosshair), lifeTime);
                audioSource.PlayOneShot(laserSound);
            }
        }
    }

    private bool CanFire(float angle)
    {
        for (int i = 0; i < firingAngle.Length; ++i)
        {
            if (angle > firingAngle[i] & angle < firingAngle[++i])
            {
                return true;
            }
        }
        return false;
    }

    public abstract bool CanFire();
}
