using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : ShopItem
{
    public float dirSpeed;
    public float angleSpeed;
    public override void ApplyUpgrade()
    {
        Move move = GameObject.Find("Player").GetComponent<Move>();
        move.dirSpeed += dirSpeed;
        move.angleSpeed += angleSpeed;
    }
}
