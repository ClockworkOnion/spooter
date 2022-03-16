using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDropper : Weapon
{
    protected override bool CanFire(float angle)
    {
        return true;
    }

    protected override float FiringAngle()
    {
        return 0;
    }

    protected override bool Powered()
    {
        return true;
    }

    protected override bool Refire()
    {
        return (lastShot += Time.deltaTime) > refireRate && Input.GetKey(KeyCode.E);
    }

    protected override Vector2 Velocity(float angle)
    {
        return shipBody.velocity;
    }
}
