using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashDamage : DirectionalDamage
{
    private AudioSource audio;
    public AudioClip crashSound;

    public void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public override float GetCollisionDamage(Collision2D collision, ContactPoint2D contact)
    {
        if (audio != null) { audio.PlayOneShot(crashSound); }
        return contact.relativeVelocity.magnitude * (contact.otherRigidbody.mass * 1000) * (contact.rigidbody.mass * 1000);
    }
}
