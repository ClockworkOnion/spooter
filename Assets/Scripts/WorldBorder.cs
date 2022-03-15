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

    GameObject canvas;

    private float worldSizeHalf;
    private float damageZoneSizeHalf;

    private void Awake()
    {
        worldSizeHalf = worldSize / 2;
        damageZoneSizeHalf = damageZoneSize / 2;
        BoxCollider2D[] boxColliders = GetComponents<BoxCollider2D>();
        boxColliders[0].offset = new Vector2(0, worldSizeHalf + damageZoneSizeHalf);
        boxColliders[0].size = new Vector2(worldSize + damageZoneSize * 2, damageZoneSize);

        boxColliders[1].offset = new Vector2(0, -worldSizeHalf -damageZoneSizeHalf);
        boxColliders[1].size = new Vector2(worldSize + damageZoneSize * 2, damageZoneSize);

        boxColliders[2].offset = new Vector2(worldSizeHalf + damageZoneSizeHalf, 0);
        boxColliders[2].size = new Vector2(damageZoneSize, worldSize);

        boxColliders[3].offset = new Vector2(-worldSizeHalf - damageZoneSizeHalf, 0);
        boxColliders[3].size = new Vector2(damageZoneSize, worldSize);
    }

    private void Start()
    {
        canvas = GameObject.Find("WorldBorder/Canvas");
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out DamageModel model))
        {
            Vector2 dirVec;
            if(collision.transform.position.x < -worldSizeHalf)
            {
                dirVec = Vector2.left;
            } else if (collision.transform.position.x > worldSizeHalf)
            {
                dirVec = Vector2.right;
            } else if (collision.transform.position.y > worldSizeHalf)
            {
                dirVec = Vector2.up;
            } else 
            {
                dirVec = Vector2.down;
            }

            DamageDirection dir = DamageModel.AngleToDirection(Vector2.SignedAngle(collision.transform.up, dirVec));
            model.Damage(damage * Time.fixedDeltaTime, dir);
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
        } else if (collision.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
}
