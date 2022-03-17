using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileLauncher : DirectionalWeapon
{

    [Range(0f, 100f)]
    public float drain;
    private Crosshair crosshair;
    public int volleySize = 4;
    public float missilePrecision = 1f;
    private AudioSource audio;
    public AudioClip rocketSound;

    protected override void Start()
    {
        base.Start();
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        audio = GetComponent<AudioSource>();
    }
    protected override bool Powered()
    {
        return systems.useEnergy(drain);
    }

    protected override void Update()
    {
        if (Refire() && crosshair.ClosestEnemy() != null)
        {
            FireVolley(crosshair.ClosestEnemy().transform);
            StartCoroutine(DelayedVolley(0.25f, crosshair.ClosestEnemy().transform));
        }
    }

    private void FireVolley(Transform target)
    {
        for (int i = 0; i < volleySize; i++ )
        {
            float spread = Random.Range(-2, 2);
            Vector3 spreadVector = new Vector3(spread, spread, spread);
            float angle = FiringAngle() + Random.Range(-20, 20);
            if (CanFire(angle) && Powered()) {
                audio.PlayOneShot(rocketSound);
                lastShot = 0;
                GameObject shot = Instantiate(projectileType, transform.position + spreadVector,
                    Quaternion.AngleAxis(-angle, Vector3.forward));
                shot.GetComponent<GuidedMissile>().SetPrecision(missilePrecision);
                Destroy(shot, lifeTime);
                if (shot.TryGetComponent(out Rigidbody2D body))
                {
                    body.velocity = Velocity(angle);
                }
                shot.GetComponent<GuidedMissile>().SetTarget(target);
            }
        }
    }

    protected override bool Refire()
    {
        return (lastShot += Time.deltaTime) > refireRate && Input.GetKeyDown("q");
    }
    
    IEnumerator DelayedVolley(float delay, Transform target)
    {
        yield return new WaitForSeconds(delay);
        if (target != null) FireVolley(target);
    }
}
