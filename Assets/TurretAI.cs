using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform player; // Refer�ncia al jugador.
    public float detectionRange; // Rang de detecci�.
    private bool isEngagingPlayer = false; // Bandera per controlar l'estat.
    public float rotationSpeed = 5f; // Velocitat de rotaci� per a un moviment suau.

    public LineRenderer lineRenderer; // Refer�ncia al LineRenderer (assignar al inspector).
    public Color laserColor = Color.red; // Color del l�ser.
    public float laserWidth = 0.05f; // Gruix del l�ser.
    public float damagePerSecond = 10f; // Dany que infligeix per segon.

    private PlayerHealth playerHealth; // Sistema de salut del jugador.

    public AudioSource turretSound; // Refer�ncia al component d'�udio.

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
                    lineRenderer.enabled = true; // Activar el l�ser.
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
                    lineRenderer.enabled = false; // Desactivar el l�ser.

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
        // Calcular la direcci� cap al jugador.
        Vector3 direction = (player.position - transform.position).normalized;

        // Ignorar la difer�ncia en l'eix Y per evitar rotacions rares (nom�s rotar sobre l'eix Y).
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Interpolar la rotaci� suaument amb Slerp.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        Debug.Log("Apuntant al jugador...");
    }

    void DrawLaser()
    {
        // Configurar els punts del l�ser.
        lineRenderer.SetPosition(0, transform.position); // Punt inicial: la torreta.
        lineRenderer.SetPosition(1, player.position); // Punt final: el jugador.
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
