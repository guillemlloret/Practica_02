using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameObject lightPrefab; // Prefab de la llum
    public Transform player; // Transform del jugador
    public int numberOfLights = 10; // Quantitat de llums a generar
    public float minY = 0f; // Mínima posició Y
    public float maxY = 5f; // Màxima posició Y
    public float minZ = 10f; // Mínima posició Z
    public float maxZ = 50f; // Màxima posició Z
    public float detectionRadius = 5f; // Radi de detecció del jugador
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

            newLight.SetActive(false); // La llum comença apagada
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
                    if (!light.activeSelf) // Si la llum no està activa, la podem encendre
                    {
                        light.SetActive(true); // Encén la llum si el jugador està a prop

                        // Rotació de la llum cap al jugador (només una vegada)
                        Vector3 directionToPlayer = (player.position - light.transform.position).normalized;
                        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                        light.transform.rotation = lookRotation;
                    }
                }
            }
        }
    }
}
