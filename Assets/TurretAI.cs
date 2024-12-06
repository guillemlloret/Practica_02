using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform player; // Referencia al jugador.
    public float detectionRange = 10f; // Rango de detección.
    private bool isEngagingPlayer = false; // Bandera para controlar el estado.
    public float rotationSpeed = 5f; // Velocidad de rotación para que no sea brusca.

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Cambiar al modo de apuntar al jugador
                if (!isEngagingPlayer)
                {
                    isEngagingPlayer = true;
                }
                EngagePlayer();
            }
            else
            {
                // Cambiar al modo sin movimiento (sin apuntar al jugador)
                if (isEngagingPlayer)
                {
                    isEngagingPlayer = false;
                }
            }
        }
    }

    void EngagePlayer()
    {
        // Calcular la dirección hacia el jugador.
        Vector3 direction = (player.position - transform.position).normalized;

        // Ignorar la diferencia en el eje Y para evitar rotaciones raras (solo rotar sobre el eje Y).
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Interpolar la rotación suavemente usando Slerp.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Esto ayudará a que la torreta se alinee de manera más suave y no sea tan brusco.
        Debug.Log("Apuntando al jugador...");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}






