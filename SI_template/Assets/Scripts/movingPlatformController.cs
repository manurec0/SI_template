using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformController : MonoBehaviour
{
    public float speed = 2.0f; 
    private float startPositionZ= 85.0f; 
    private float endPositionZ= 75.0f; 
    private bool movingForward = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Mover la plataforma hacia adelante y hacia atrás en el eje Z
        if (movingForward)
        {
            // Mover hacia adelante
            if (transform.position.z > endPositionZ)
            {
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            }
            else
            {
                movingForward = false; // Cambiar de dirección
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
