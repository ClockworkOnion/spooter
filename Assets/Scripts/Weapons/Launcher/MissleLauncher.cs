using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncher : DirectionalWeapon
{
    [Range(0f, 100f)]
    public float drain;
    protected override bool Powered()
    {
        return systems.useEnergy(drain);
    }
}
