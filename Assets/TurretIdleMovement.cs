using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleMovement : MonoBehaviour
{
    public bool isIdle = true; 
    public float rotationSpeed = 30f; 
    private float currentRotation = 0f; 
    private float rotationLimit = 60f; 
    private int rotationDirection = 1; 

    void Update()
    {
        if (isIdle)
        {
            
            float rotationStep = rotationSpeed * Time.deltaTime * rotationDirection;

            
            currentRotation += rotationStep;

           
            if (Mathf.Abs(currentRotation) >= rotationLimit)
            {
                rotationDirection *= -1;
                currentRotation = Mathf.Clamp(currentRotation, -rotationLimit, rotationLimit);
            }

           
            transform.Rotate(0, rotationStep, 0);
        }
    }
}

