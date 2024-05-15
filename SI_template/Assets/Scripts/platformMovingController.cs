using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovingController : MonoBehaviour, IPlateAction
{
    public float speed = 2.0f;
    private float startPositionZ = 85.0f;
    private float endPositionZ = 75.0f;
    private bool movingForward = true;
    private bool playerOnButton = false;

    void Update()
    {
        if (!playerOnButton) // Solo moverse si el jugador no está presionando el botón
        {
            if (movingForward)
            {
                // Move forward
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
                // Move backward
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

    // Implementación de la interfaz IPlateAction
    public void ExecuteAction(bool isActive)
    {
        playerOnButton = isActive; // Cambia el estado de si el jugador está presionando el botón
    }
}
