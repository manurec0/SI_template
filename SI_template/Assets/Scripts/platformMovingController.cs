using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovingController : MonoBehaviour
{
    public float speed = 2.0f;
    private float startPositionZ = 85.0f;
    private float endPositionZ = 75.0f;
    private bool movingForward = true;
    public GameObject platform;
    private bool playerOnButton = false;

    void Update()
    {
        if (!playerOnButton)
        {
            if (movingForward)
            {
                // Move forward
                if (platform.transform.position.z > endPositionZ)
                {
                    platform.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
                }
                else
                {
                    movingForward = false;
                }
            }
            else
            {
                // Move backward
                if (platform.transform.position.z < startPositionZ)
                {
                    platform.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                }
                else
                {
                    movingForward = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        playerOnButton = !playerOnButton;
        
    }
}
