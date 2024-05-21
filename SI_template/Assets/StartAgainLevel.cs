using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAgainLevel : MonoBehaviour
{

    private bool player1IsStart = false;
    private bool player2IsStart = false;
    public GameObject canvas;

    public GameObject message;

    private GameObject colliders;
    private GameObject EndTilesObj;

    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(true);
        canvas.SetActive(false);

        player1IsStart = false;
        player2IsStart = false;

        Transform parentTransform = transform.parent;
        Transform childTransform = parentTransform.Find("colliders");
        colliders = childTransform.gameObject;

        Transform endTilesTransform = parentTransform.Find("EndTiles");
        EndTilesObj = endTilesTransform.gameObject;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsStart = true;
            Debug.Log("Player 1 reached the start tile");
        }

        if (other.CompareTag("Player2"))
        {
            player2IsStart = true;
            Debug.Log("Player 2 reached the start tile");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsStart = false;
            Debug.Log("Player 1 left the start tile");
        }

        if (other.CompareTag("Player2"))
        {
            player2IsStart = false;
            Debug.Log("Player 2 left the start tile");
        }
    }

    private void Update()
    {
        if (player1IsStart && player2IsStart)
        {
            //activate colliders
            colliders.SetActive(true);

            //activate colliders end tiles
            BoxCollider[] boxCollidersEnd = EndTilesObj.GetComponents<BoxCollider>();
            foreach (BoxCollider boxCollider in boxCollidersEnd) boxCollider.enabled = true;

            // remove message
            message.SetActive(false);
            canvas.SetActive(true);

            //disable colliders start tiles
            BoxCollider[] boxCollidersStart = GetComponents<BoxCollider>();
            foreach (BoxCollider boxCollider in boxCollidersStart) boxCollider.enabled = false;

            //idk if this will break the program as it is disabling this same script
            GetComponent<MonoBehaviour>().enabled = false;



        }

    }

}
