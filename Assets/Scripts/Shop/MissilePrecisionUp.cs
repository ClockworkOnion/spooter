using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePrecisionUp : ShopItem
{
    public float factor = 0.7f;

    public override void ApplyUpgrade()
    {
        GuidedMissileLauncher launcher = GameObject.FindGameObjectWithTag("Player").transform.Find("Tower").GetComponent<GuidedMissileLauncher>();

        launcher.missilePrecision *= factor;
    }
}
