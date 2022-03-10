using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : AimedWeapon
{

    protected override bool Powered()
    {
        return true;
    }
}
