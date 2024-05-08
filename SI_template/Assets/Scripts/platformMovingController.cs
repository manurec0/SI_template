using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovingController : MonoBehaviour
{
    public float speed = 2.0f; 
    private float startPositionZ= 85.0f; 
    private float endPositionZ= 75.0f; 
    private bool movingForward = true;

    void Start()
    {
    }

    void Update()
    {
        if (movingForward)
        {
            // Mover hacia adelante
            if (transform.position.z > endPositionZ)
            {
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            }
            else
            {
                movingForward = false; 
            }
        }
        else
        {
            // Mover hacia atrás
            if (transform.position.z < startPositionZ)
            {
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            }
            else
            {
                movingForward = true; 
            }
        }
    }
}
