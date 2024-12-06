using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Destruye el proyectil al impactar.
    }

    void Start()
    {
        Destroy(gameObject, 5f); // Destruye tras 5 segundos si no impacta.
    }
}

