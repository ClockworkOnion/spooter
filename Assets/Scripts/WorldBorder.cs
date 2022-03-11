using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBorder : MonoBehaviour
{
    [Range(100f, 20000f)]
    public float worldSize;
    [Range(100f, 2000f)]
    public float damageZoneSize;
    [Range(1f, 200f)]
    public float damage;

    private float worldSizeHalf;

    private void Awake()
    {
        worldSizeHalf = worldSize / 2;
        BoxCollider2D[] boxColliders = GetComponents<BoxCollider2D>();
        boxColliders[0].offset = new Vector2(0, worldSizeHalf);
        boxColliders[0].size = new Vector2(worldSize, damageZoneSize);

        boxColliders[1].offset = new Vector2(0, -worldSizeHalf);
        boxColliders[1].size = new Vector2(worldSize, damageZoneSize);

        boxColliders[2].offset = new Vector2(worldSizeHalf, 0);
        boxColliders[2].size = new Vector2(damageZoneSize, worldSize);

        boxColliders[3].offset = new Vector2(-worldSizeHalf, 0);
        boxColliders[3].size = new Vector2(damageZoneSize, worldSize);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out DamageModel model))
        {
            model.Damage(damage * Time.fixedDeltaTime, DamageDirection.Front); //TODO
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.position.x > worldSizeHalf || collision.transform.position.x < -worldSizeHalf
            || collision.transform.position.y > worldSizeHalf || collision.transform.position.y < -worldSizeHalf)
        {
            if (collision.gameObject.TryGetComponent(out DamageModel model))
            {
                model.OnHullDestroyed();
            }
        }
    }
}
