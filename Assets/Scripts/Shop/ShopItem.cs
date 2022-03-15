using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : MonoBehaviour
{
    public Sprite texture;
    public string text;

    public abstract void ApplyUpgrade();
}
