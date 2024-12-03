using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        //afegim els plurals per a que pugui fer m�s d'un canvi
        //enlloc del array podriem posar var tamb�, perque ja hem definit el que �s
        //el var necessita que li diguem el tipus de variable
        ITakeDamage[] damageTakers =
            collision.collider.GetComponents<ITakeDamage>();

        if (damageTakers != null)
        {
            foreach (var item in damageTakers)
            {
                item.TakeDamage(Damage);
            }
        }

    }
}
