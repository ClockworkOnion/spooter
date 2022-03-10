using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageDirection
{
    Front,Right,Back,Left

}

public abstract class DamageModel : MonoBehaviour
{
    [Range(0f, 1000f)]
    public float maxHull = 100;
    [Range(0f, 1000f)]
    public float[] maxShield = new float[4];

    float[] shield = new float[4];
    float hull;

    public float Hull
    {
        get => hull;
    }

    public float Shield(DamageDirection direction)
    {
        return shield[(int)direction];
    }

    public float[] Shields(float[] shields)
    {
        Array.Copy(shield, shields, 4);
        return shields;
    }

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
        hull = Mathf.Min(hull + amount, maxHull);
    }

    public void RechargeShields(float amount)
    {
        int notFull = shield.Length;
        for (int i = 0; i < shield.Length; i++)
        {
            if(shield[i] == maxShield[i])
            {
                --notFull;
            }
        }
        for (int i = 0; i < shield.Length; i++)
        {
            if (shield[i] != maxShield[i])
            {
                shield[i] = Mathf.Min(maxShield[i], shield[i] + amount / notFull);
            }
        }
    }
    
    public static DamageDirection AngleToDirection(float angle)
    {
        return angle < -135? DamageDirection.Back :
            angle < -45 ? DamageDirection.Right :
            angle < 45 ? DamageDirection.Front :
            angle < 135 ? DamageDirection.Left :
            DamageDirection.Back;
    }
}
