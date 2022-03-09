using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageDirection
{
    Front,Right,Back,Left

}

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class DamageModel : MonoBehaviour
{
    public float maxHull = 100;
    public float[] maxShield = new float[4];
    public float[] shield = new float[4];
    public float hull;

    public void Start()
    {
        hull = maxHull;
        Array.Copy(maxShield, shield, 4);
    }

    public void Damage(float amount, DamageDirection direction)
    {
        float dirShield = shield[((int)direction)];
        if(dirShield > 0)
        {
            dirShield -= amount;
            if(dirShield >= 0)
            {
                shield[((int)direction)] = dirShield;
                return;
            } 
            else
            {
                shield[((int)direction)] = 0;
                amount = -dirShield;
            }
        }
        hull -= amount;
        if(hull < 0)
        {
            OnHullDestroyed();
        }
    }

    public abstract void OnHullDestroyed();

    public void RepairHull(float amount)
    {
        hull = Mathf.Max(hull + amount, maxHull);
    }
    
    public static DamageDirection AngleToDirection(float angle)
    {
        return angle < -135? DamageDirection.Back :
            angle < -45 ? DamageDirection.Right :
            angle < 45 ? DamageDirection.Front :
            angle < 135 ? DamageDirection.Left :
            DamageDirection.Back;
    }

    public static DamageDirection AttackDirection(Transform attacked, Transform attacker)
    {
        return AngleToDirection(Vector2.SignedAngle(attacked.up,
                attacker.position - attacked.position));
    }
}
