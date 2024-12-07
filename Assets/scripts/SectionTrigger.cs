using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public float moveStep; 
    public int stepCount = 0; 
    public GameObject roadSection;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            stepCount += 1;
            Instantiate(roadSection, new Vector3(0, 0, moveStep * stepCount), Quaternion.identity);
        }
    }
}
