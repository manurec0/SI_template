using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovingController : MonoBehaviour, IPlateAction
{
    public float speed = 2.0f;

    public Vector3 endPos;
    public Vector3 direction;

    private Vector3 startPos;
    private Vector3 debugCurrPos;
    private float debugDistanceStart;
    private float debugDistanceEnd;
    private Vector3 currDirection;

    //private bool movingForward = true;
    private bool playerOnButton = false;
    public bool isMoving;

    //Audio
    public AudioSource movingLoop;
    public AudioSource startUp;
    public AudioSource end;

    void Start()
    {
        startPos = transform.position;
        debugCurrPos = startPos;
        movingLoop.Play();

    }
    void Update()
    {
        BoxCollider boxCollider = GetComponentInParent<BoxCollider>();

        if (!playerOnButton) // Solo moverse si el jugador no está presionando el botón
        {
            isMoving = true;
            //movingLoop.Play();

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
        if ((transform.position - endPos).magnitude < 5f && playerOnButton)
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
        if (playerOnButton)
        {
            StartCoroutine(AudioManager.FadeOut(movingLoop, 1f));
            end.Play();
        }
        else
        {
            startUp.Play();
            movingLoop.Play();
        }
        isMoving = !isMoving;
    }
}
