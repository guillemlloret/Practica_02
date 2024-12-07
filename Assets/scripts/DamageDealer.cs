using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Damage = 5;

    private void OnCollisionEnter(Collision collision)
    {

        ITakeDamage[] damageTakers = collision.collider.GetComponents<ITakeDamage>();

        if (damageTakers != null)
        {
            Debug.Log("xoc");
            foreach (var item in damageTakers)
            {
                item.TakeDamage(Damage);
            }
        }

    }
}
