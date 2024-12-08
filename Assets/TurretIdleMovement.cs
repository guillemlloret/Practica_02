using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleMovement : MonoBehaviour
{
    public bool isIdle = true; // Controla si la torreta està en estat idle.
    public float rotationSpeed = 30f; // Velocitat de rotació.
    private float currentRotation = 0f; // Anglè actual de rotació.
    private float rotationLimit = 60f; // Límits de rotació a cada costat.
    private int rotationDirection = 1; // Direcció de rotació (1 = dreta, -1 = esquerra).

    void Update()
    {
        if (isIdle)
        {
            // Calcula el moviment de rotació segons la direcció i la velocitat.
            float rotationStep = rotationSpeed * Time.deltaTime * rotationDirection;

            // Actualitza la rotació actual.
            currentRotation += rotationStep;

            // Inverteix la direcció si s'arriba al límit.
            if (Mathf.Abs(currentRotation) >= rotationLimit)
            {
                rotationDirection *= -1;
                currentRotation = Mathf.Clamp(currentRotation, -rotationLimit, rotationLimit);
            }

            // Aplica la rotació a l'objecte.
            transform.Rotate(0, rotationStep, 0);
        }
    }
}

