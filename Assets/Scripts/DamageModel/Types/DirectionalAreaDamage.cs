using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DirectionalAreaDamage : MonoBehaviour
{
    public float maxDps = 10;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out DamageModel model))
        {
            model.Damage(maxDps * Time.deltaTime ,
                DamageModel.AttackDirection(collision.transform, transform));
        }
        
    }
}
