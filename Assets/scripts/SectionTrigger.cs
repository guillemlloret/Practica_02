using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public float moveStep; 
    public int stepCount = 0; 
    public GameObject roadSection;
    public GameObject roadSection2;
    public GameObject roadSection3;
    public PlayerHealth player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            if (player._currentHealth < 50 && player._currentHealth >25)
            {
                Debug.Log("sota 50");
                stepCount += 1;
                Instantiate(roadSection2, new Vector3(0, 5, moveStep * stepCount), Quaternion.identity);
            }
            else if (player._currentHealth < 25)
            {
                Debug.Log("per sota 25");
                stepCount += 1;
                Instantiate(roadSection3, new Vector3(0, 5, moveStep * stepCount), Quaternion.identity);
            }

            else
            {
                stepCount += 1;
                Instantiate(roadSection, new Vector3(0, 5, moveStep * stepCount), Quaternion.identity);
            }
        }

        
    }
}