using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform player; // Referencia al jugador.
    public float detectionRange; // Rango de detección.
    private bool isEngagingPlayer = false; // Bandera para controlar el estado.
    public float rotationSpeed = 5f; // Velocidad de rotación para que no sea brusca.

    public LineRenderer lineRenderer; // Referencia al LineRenderer (asignar en el inspector).
    public Color laserColor = Color.red; // Color del láser.
    public float laserWidth = 0.05f; // Grosor del láser.
    public float damagePerSecond = 10f; // Daño que inflige por segundo.

    private PlayerHealth playerHealth; // Sistema de salud del jugador.

    void Start()
    {
        //if (lineRenderer == null)
        //{
        //    // Obtener el LineRenderer si no se asignó manualmente.
        //    lineRenderer = GetComponent<LineRenderer>();
        //}

        // Configurar el LineRenderer.
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = laserColor;
        lineRenderer.enabled = false; // Inicialmente desactivado.
    }

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
                    Debug.Log("Jugador detectado en el rango de la torreta.");
                    lineRenderer.enabled = true; // Activar el láser.
                    playerHealth = player.GetComponent<PlayerHealth>(); // Obtener el sistema de salud.
                }

                EngagePlayer();
                DrawLaser();

                // Aplicar daño continuo al jugador.
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
            else
            {
                // Cambiar al modo sin movimiento (sin apuntar al jugador)
                if (isEngagingPlayer)
                {
                    isEngagingPlayer = false;
                    Debug.Log("Jugador salió del rango de la torreta.");
                    lineRenderer.enabled = false; // Desactivar el láser.
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

        Debug.Log("Apuntando al jugador...");
    }

    void DrawLaser()
    {
        // Configurar los puntos del láser.
        lineRenderer.SetPosition(0, transform.position); // Punto inicial: la torreta.
        lineRenderer.SetPosition(1, player.position); // Punto final: el jugador.
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
