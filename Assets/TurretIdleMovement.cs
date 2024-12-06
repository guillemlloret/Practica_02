using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleMovement : MonoBehaviour
{
    public bool isIdle = true; // Controla si la torreta est� en estado de idle.

    void Update()
    {
        if (isIdle)
        {
            // Rotaci�n continua en estado idle.
            transform.Rotate(0, 30f * Time.deltaTime, 0);
        }
    }
}


