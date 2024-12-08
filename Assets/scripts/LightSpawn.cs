using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameObject lightPrefab; // Prefab de la llum
    public Transform player; // Transform del jugador
    public int numberOfLights = 10; // Quantitat de llums a generar
    public float minY = 0f; // M�nima posici� Y
    public float maxY = 5f; // M�xima posici� Y
    public float minZ = 10f; // M�nima posici� Z
    public float maxZ = 50f; // M�xima posici� Z
    public float detectionRadius = 5f; // Radi de detecci� del jugador
    public float[] xValues = new float[] { -2f, 2f }; // Valors possibles per X

    private List<GameObject> lights = new List<GameObject>();

    void Start()
    {
        GenerateLights();
    }

    void Update()
    {
        CheckPlayerProximity();
    }

    private void GenerateLights()
    {
        for (int i = 0; i < numberOfLights; i++)
        {
            float randomX = xValues[Random.Range(0, xValues.Length)];
            float randomY = Random.Range(minY, maxY);
            float randomZ = Random.Range(minZ, maxZ);

            Vector3 position = new Vector3(randomX, randomY, randomZ);
            GameObject newLight = Instantiate(lightPrefab, position, Quaternion.identity);

            newLight.SetActive(false); // La llum comen�a apagada
            lights.Add(newLight);
        }
    }

    private void CheckPlayerProximity()
    {
        foreach (GameObject light in lights)
        {
            if (light != null)
            {
                float distanceToPlayer = Vector3.Distance(player.position, light.transform.position);

                if (distanceToPlayer <= detectionRadius)
                {
                    if (!light.activeSelf) // Si la llum no est� activa, la podem encendre
                    {
                        light.SetActive(true); // Enc�n la llum si el jugador est� a prop

                        // Rotaci� de la llum cap al jugador (nom�s una vegada)
                        Vector3 directionToPlayer = (player.position - light.transform.position).normalized;
                        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                        light.transform.rotation = lookRotation;
                    }
                }
            }
        }
    }
}
