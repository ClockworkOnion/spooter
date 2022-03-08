using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float maxDps = 10;

    private void OnTriggerStay2D(Collider2D collision)
    {
        DamageModel model;
        if(collision.TryGetComponent(out model))
        {
            model.Damage(maxDps * Time.deltaTime ,
                DamageModel.AttackDirection(collision.transform, transform));
        }
        
    }
}
