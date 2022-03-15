using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [Range(0f, 20f)]
    public float lifeTime;
    [Range(0, 1)]
    public int weapon;
    [Range(0f, 3f)]
    public float baseRefireRate;
    public float refireRate;
    [Range(1f, 1000f)]
    public float speed;
    public AudioClip laserSound;
    public GameObject projectileType;
    

    protected Rigidbody2D shipBody;
    protected AudioSource audioSource;
    protected ShipSystems systems;
    protected float lastShot;

    protected virtual void Start()
    {
        refireRate = baseRefireRate;
        shipBody = GetComponentInParent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        systems = GetComponentInParent<ShipSystems>();
    }

    protected virtual void Update()
    {
        if(Refire())
        {
            float angle = FiringAngle();
            if (CanFire(angle) && Powered()) {
                lastShot = 0;
                GameObject shot = Instantiate(projectileType, transform.position,
                    Quaternion.AngleAxis(-angle, Vector3.forward));
                Destroy(shot, lifeTime);
                if (shot.TryGetComponent(out Rigidbody2D body))
                {
                    body.velocity = Velocity(angle);
                }
                // audioSource.PlayOneShot(laserSound);
            }
        }
    }

    protected virtual bool Refire()
    {
        return (lastShot += Time.deltaTime) > refireRate && Input.GetMouseButton(weapon);
    }

    protected virtual Vector2 Velocity(float angle) 
    {
        angle *= Mathf.Deg2Rad;
        return shipBody.velocity + new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * speed;
    }

    // Firing arc
    protected abstract bool CanFire(float angle);

    // Check for Power and Power drain
    protected abstract bool Powered();

    // FiringAngle in world coordinates
    protected abstract float FiringAngle();
}
