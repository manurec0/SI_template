using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    public GameObject manageLevels;
    public TextMeshProUGUI levelCounterObj;
    public int counter;

    private void Start()
    {
        //initialize the Global counter
        counter = -1;
        levelCounterObj.text = counter.ToString();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("outPath"))
        {
            TransitionLevels();
        }
    }


    public void setPosition(Vector3 pos)
    {
        //swith playerIndex
        transform.position = pos;
    }

    public void setRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }


    void TransitionLevels()
    {
        Transform prevTrans = manageLevels.transform.GetChild(counter-1);
        Transform currTrans = manageLevels.transform.GetChild(counter);
        GameObject prevEndTile = prevTrans.gameObject;
        GameObject currEndTile = currTrans.gameObject;
        counter--;
        levelCounterObj.text = counter.ToString();

        //change endTiles 
        prevEndTile.SetActive(true);
        LevelChange.LevelUp(counter); //this should reset the local variables of everything (for the end tiles prev if u fell level 1 wouldnt get updated as it was disabled lets see)
        currEndTile.SetActive(false);


        //disable colliders of the level until both players are at the start positions again
        Transform childTransform = prevEndTile.transform.Find("colliders");
        GameObject colliders = childTransform.gameObject;
        colliders.SetActive(false);

        //get the start and end tiles object
        Transform startTilesTransform = prevEndTile.transform.Find("StartTiles");
        GameObject startTiles = startTilesTransform.gameObject;
        Transform endTilesTransform = prevEndTile.transform.Find("EndTiles");
        GameObject endTiles = endTilesTransform.gameObject;

        //disable colliders end tiles
        BoxCollider[] boxCollidersEnd  = endTiles.GetComponents<BoxCollider>();
        foreach (BoxCollider boxCollider in boxCollidersEnd) boxCollider.enabled = false;

        //activate colliders start tiles
        BoxCollider[] boxCollidersStart = startTiles.GetComponents<BoxCollider>();
        foreach (BoxCollider boxCollider in boxCollidersStart) boxCollider.enabled = true;

        //activate the script
        startTiles.GetComponent<MonoBehaviour>().enabled = true;
        //make the change of levels and is true as we have lost and want to go up
        currEndTile.transform.GetChild(2).position = new Vector3(0, 1000, 0);
        LevelChange.TriggerMoveObject(true, endTilesTransform);
    }


    void OnEnable()
    {
        LevelChange.OnLevelUp += UpdateLocalCounter;
    }

    void OnDisable()
    {
        LevelChange.OnLevelUp -= UpdateLocalCounter;
    }

    private void UpdateLocalCounter(int newLevel)
    {
        counter = newLevel;

    }

   
}
