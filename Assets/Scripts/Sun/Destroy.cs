using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageModel model;
        if (collision.TryGetComponent(out model)) model.OnHullDestroyed();
    }
}
