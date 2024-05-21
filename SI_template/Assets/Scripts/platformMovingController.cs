using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovingController : MonoBehaviour, IPlateAction
{
    public float speed = 2.0f;
    //private float startPositionZ = 85.0f;
    //private float endPositionZ = 75.0f;
    private float startPositionZ;
    public float endPositionZ;

    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 direction;

    private Vector3 debugCurrPos;
    private float debugDistanceStart;
    private float debugDistanceEnd;
    private Vector3 currDirection;

    //private bool movingForward = true;
    private bool playerOnButton = false;

    void Start()
    {
        startPos = transform.position;
        startPositionZ = startPos.z;
        debugCurrPos = startPos;

    }
    void Update()
    {
        BoxCollider boxCollider = GetComponentInParent<BoxCollider>();

        if (!playerOnButton) // Solo moverse si el jugador no está presionando el botón
        {

            if (debugDistanceStart < 0.25f)
            {
                //move forward
                currDirection = direction; 
            }
            if (debugDistanceEnd < 0.25f)
                currDirection = direction * -1;

            //move platform
            startPos.y = transform.position.y;
            debugCurrPos = transform.position;
            endPos.y = debugCurrPos.y;
            debugDistanceStart = Vector3.Distance(debugCurrPos, startPos);
            debugDistanceEnd = Vector3.Distance(debugCurrPos, endPos);
            transform.position += currDirection * speed * Time.deltaTime;

            
        }

        //remove the collider fall if platform placed correctly
        if ((transform.position - endPos).magnitude < 0.5f && playerOnButton)
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
    }

    // Implementación de la interfaz IPlateAction
    public void ExecuteAction(bool isActive)
    {
        playerOnButton = isActive; // Cambia el estado de si el jugador está presionando el botón
    }
}
