using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float distance;
    public float Speed = 5f;
   

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, Target.transform.position);
        if (distance >= 4)
        {
           

            transform.position =  Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }
    }
}
