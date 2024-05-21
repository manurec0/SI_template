using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{

    public List<GameObject> levelsList;
    public AnimationCurve speedCurve;
    public float duration = 2f;

    public GameObject manageLevels;

    public TextMeshProUGUI levelCounterObj;
    private int counter;

    private void Start()
    {
        counter = 0;
        levelCounterObj.text = counter.ToString();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("outPath") && counter == 0) 
        {
            //here should be animation of death badabi badaba
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }
        else if (other.CompareTag("outPath"))
        {

            StartCoroutine(TransitionLevels(levelsList[counter - 1], levelsList[counter]));
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


    IEnumerator TransitionLevels(GameObject prevLevel, GameObject currentLevel)
    {
        Transform prevTrans = manageLevels.transform.GetChild(counter-1);
        Transform currTrans = manageLevels.transform.GetChild(counter);
        GameObject prevEndTile = prevTrans.gameObject;
        GameObject currEndTile = currTrans.gameObject;
        counter--;




        //change endTiles 
        prevEndTile.SetActive(true);

        Debug.Log($"previous should be active {prevEndTile.name}, state: {prevEndTile.activeInHierarchy}");
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


        //animation of falling currently theres no animation but it does the change of level...
        levelCounterObj.text = counter.ToString();
        float startTime = Time.time;
        Vector3 currentLevelStartPos = currentLevel.transform.position;
        Vector3 nextLevelStartPos = prevLevel.transform.position;
        Vector3 currentLevelEndPos = currentLevelStartPos + new Vector3(0, 1000, 0);
        Vector3 nextLevelEndPos = nextLevelStartPos + new Vector3(0, 1000, 0);

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float curveValue = speedCurve.Evaluate(t);
            currentLevel.transform.position = Vector3.Lerp(currentLevelEndPos, currentLevelStartPos, curveValue);
            prevLevel.transform.position = Vector3.Lerp(nextLevelEndPos, nextLevelStartPos,  curveValue);
            yield return null;
        }

        currentLevel.transform.position = currentLevelEndPos;
        prevLevel.transform.position = nextLevelEndPos;
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
