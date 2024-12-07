using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    public GameObject lightPrefab; // Prefab de la llum a instanciar
    public PlayerMovement playerMovement; // Referència al script de moviment del jugador
    public float distanceBetweenLights = 10f; // Distància entre les llums

    private float nextLightZ; // La propera posició Z on es generarà una llum

    void Start()
    {
        if (playerMovement == null)
        {
            Debug.LogError("No s'ha assignat el PlayerMovement!");
            return;
        }

        // Inicialitzem la primera posició Z per instanciar una llum
        nextLightZ = Mathf.Floor(playerMovement.transform.position.z / distanceBetweenLights) * distanceBetweenLights + distanceBetweenLights;
    }

    void Update()
    {
        if (playerMovement.transform.position.z >= nextLightZ)
        {
            // Instanciar la llum
            Instantiate(lightPrefab, new Vector3(playerMovement.transform.position.x, playerMovement.transform.position.y + 2f, nextLightZ), Quaternion.identity);

            // Actualitzar la propera posició Z
            nextLightZ += distanceBetweenLights;
        }
    }
}