using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAgainLevel : MonoBehaviour
{

    private bool player1IsStart = false;
    private bool player2IsStart = false;

    public GameObject canvas;
    public GameObject message;
    public GameObject level; //level path to disable colliders of moving and cracked tiles

    private GameObject colliders;
    private GameObject EndTilesObj;

    //if for a certain level one of the paths doesnt have a moving or cracked tile the corresponding list will be empty
    private List<GameObject> movingObjs1;
    private List<GameObject> crackedObjs1;

    private List<GameObject> movingObjs2;
    private List<GameObject> crackedObjs2;

    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(true);
        canvas.SetActive(false);


        player1IsStart = false;
        player2IsStart = false;


        //initialize the gameObjects
        Transform parentTransform = transform.parent; //get the transform of level x in ManageEndTiles
        Transform childTransform = parentTransform.Find("colliders"); 
        colliders = childTransform.gameObject;

        Transform endTilesTransform = parentTransform.Find("EndTiles");
        EndTilesObj = endTilesTransform.gameObject;

        //disable those pesky colliders of the special tiles 
        Transform player1PathTrans = level.transform.GetChild(0); 
        Transform movingTiles1Trans = player1PathTrans.GetChild(0);
        Transform crackedTiles1Trans = player1PathTrans.GetChild(1);
        movingObjs1 = GetChildGameObjects(movingTiles1Trans);
        crackedObjs1 = GetChildGameObjects(crackedTiles1Trans);

        Transform player2PathTrans = level.transform.GetChild(1);
        Transform movingTiles2Trans = player2PathTrans.GetChild(0);
        Transform crackedTiles2Trans = player2PathTrans.GetChild(1);
        movingObjs2 = GetChildGameObjects(movingTiles2Trans);
        crackedObjs2 = GetChildGameObjects(crackedTiles2Trans);

        //disable the colliders
        changeAllColliders();


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
        //both players are at the start of the level so they can restart the level
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

            //activate colliders moving & and cracked tiles
            changeAllColliders();

            GetComponent<MonoBehaviour>().enabled = false;



        }

    }

    void changeAllColliders()
    {
        changeBoxColliderForCrackedTiles(crackedObjs1);
        changeBoxColliderForMovingPlatforms(movingObjs1);
        changeBoxColliderForCrackedTiles(crackedObjs2);
        changeBoxColliderForMovingPlatforms(movingObjs2);
    }

    void changeBoxColliderForCrackedTiles(List<GameObject> crackedTilesList)
    {

        foreach(GameObject obj in crackedTilesList)
        {
            Transform crackedTile = obj.transform.GetChild(0);
            Transform cracked = crackedTile.GetChild(0);
            BoxCollider tmpCollider = cracked.gameObject.GetComponent<BoxCollider>();
            tmpCollider.enabled = !tmpCollider.enabled;

        }
    }

    void changeBoxColliderForMovingPlatforms(List<GameObject> movingPlatList)
    {
        foreach(GameObject obj in movingPlatList)
        {
            Transform tileTransform = obj.transform.GetChild(0);
            MonoBehaviour script = tileTransform.gameObject.GetComponent<MonoBehaviour>();
            script.enabled = !script.enabled;

            BoxCollider tmpCollider = obj.GetComponent<BoxCollider>();

            tmpCollider.enabled = !tmpCollider.enabled;
        }
    }


    List<GameObject> GetChildGameObjects(Transform parent)
    {
        List<GameObject> childObjects = new List<GameObject>();

        // Iterate through all children of the parent object
        foreach (Transform child in parent)
        {
            // Add each child GameObject to the list
            childObjects.Add(child.gameObject);
        }

        return childObjects;
    }

}
