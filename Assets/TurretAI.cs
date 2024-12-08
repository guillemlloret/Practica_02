using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform player; 
    public float detectionRange; 
    private bool isEngagingPlayer = false; 
    public float rotationSpeed = 5f; 

    public LineRenderer lineRenderer; 
    public Color laserColor = Color.red; 
    public float laserWidth = 0.05f; 
    public float damagePerSecond = 10f; 

    private PlayerHealth playerHealth; 

    public AudioSource turretSound; 

    void Start()
    {
        
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = laserColor;
        lineRenderer.enabled = false; 

       
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
                
                if (!isEngagingPlayer)
                {
                    isEngagingPlayer = true;
                    Debug.Log("Jugador detectat dins del rang de la torreta.");
                    lineRenderer.enabled = true; 
                    playerHealth = player.GetComponent<PlayerHealth>();

                    
                    if (!turretSound.isPlaying)
                    {
                        turretSound.Play();
                    }
                }

                EngagePlayer();  //Modo apuntar jugador
                DrawLaser();

                
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
            else
            {
                
                if (isEngagingPlayer)
                {
                    isEngagingPlayer = false;
                    Debug.Log("Jugador fora del rang de la torreta.");
                    lineRenderer.enabled = false; 

                   
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
       
        Vector3 direction = (player.position - transform.position).normalized;

        
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

       
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        Debug.Log("Apuntant al jugador...");
    }

    void DrawLaser()
    {
       
        lineRenderer.SetPosition(0, transform.position); 
        lineRenderer.SetPosition(1, player.position); 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
