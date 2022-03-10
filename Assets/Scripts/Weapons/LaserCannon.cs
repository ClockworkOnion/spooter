using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : AimedWeapon
{
    public int shots = 10;
    public override bool CanFire()
    {
        return shots-- > 0;
    }
}
