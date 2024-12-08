using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform player; // Referència al jugador.
    public float detectionRange; // Rang de detecció.
    private bool isEngagingPlayer = false; // Bandera per controlar l'estat.
    public float rotationSpeed = 5f; // Velocitat de rotació per a un moviment suau.

    public LineRenderer lineRenderer; // Referència al LineRenderer (assignar al inspector).
    public Color laserColor = Color.red; // Color del làser.
    public float laserWidth = 0.05f; // Gruix del làser.
    public float damagePerSecond = 10f; // Dany que infligeix per segon.

    private PlayerHealth playerHealth; // Sistema de salut del jugador.

    public AudioSource turretSound; // Referència al component d'àudio.

    void Start()
    {
        // Configurar el LineRenderer.
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = laserColor;
        lineRenderer.enabled = false; // Inicialment desactivat.

        // Configurar el so.
        if (turretSound == null)
        {
            turretSound = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Canviar al mode d'apuntar al jugador.
                if (!isEngagingPlayer)
                {
                    isEngagingPlayer = true;
                    Debug.Log("Jugador detectat dins del rang de la torreta.");
                    lineRenderer.enabled = true; // Activar el làser.
                    playerHealth = player.GetComponent<PlayerHealth>(); // Obtenir el sistema de salut.

                    // Reproduir el so.
                    if (!turretSound.isPlaying)
                    {
                        turretSound.Play();
                    }
                }

                EngagePlayer();
                DrawLaser();

                // Aplicar dany continu al jugador.
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
            else
            {
                // Canviar al mode sense apuntar al jugador.
                if (isEngagingPlayer)
                {
                    isEngagingPlayer = false;
                    Debug.Log("Jugador fora del rang de la torreta.");
                    lineRenderer.enabled = false; // Desactivar el làser.

                    // Aturar el so.
                    if (turretSound.isPlaying)
                    {
                        turretSound.Stop();
                    }
                }
            }
        }
    }

    void EngagePlayer()
    {
        // Calcular la direcció cap al jugador.
        Vector3 direction = (player.position - transform.position).normalized;

        // Ignorar la diferència en l'eix Y per evitar rotacions rares (només rotar sobre l'eix Y).
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Interpolar la rotació suaument amb Slerp.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        Debug.Log("Apuntant al jugador...");
    }

    void DrawLaser()
    {
        // Configurar els punts del làser.
        lineRenderer.SetPosition(0, transform.position); // Punt inicial: la torreta.
        lineRenderer.SetPosition(1, player.position); // Punt final: el jugador.
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
