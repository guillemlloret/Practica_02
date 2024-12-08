using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleMovement : MonoBehaviour
{
    public bool isIdle = true; // Controla si la torreta est� en estat idle.
    public float rotationSpeed = 30f; // Velocitat de rotaci�.
    private float currentRotation = 0f; // Angl� actual de rotaci�.
    private float rotationLimit = 60f; // L�mits de rotaci� a cada costat.
    private int rotationDirection = 1; // Direcci� de rotaci� (1 = dreta, -1 = esquerra).

    void Update()
    {
        if (isIdle)
        {
            // Calcula el moviment de rotaci� segons la direcci� i la velocitat.
            float rotationStep = rotationSpeed * Time.deltaTime * rotationDirection;

            // Actualitza la rotaci� actual.
            currentRotation += rotationStep;

            // Inverteix la direcci� si s'arriba al l�mit.
            if (Mathf.Abs(currentRotation) >= rotationLimit)
            {
                rotationDirection *= -1;
                currentRotation = Mathf.Clamp(currentRotation, -rotationLimit, rotationLimit);
            }

            // Aplica la rotaci� a l'objecte.
            transform.Rotate(0, rotationStep, 0);
        }
    }
}

