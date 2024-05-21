using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class EndLevel : MonoBehaviour
{
    private bool player1IsEnd = false;
    private bool player2IsEnd = false;

    public GameObject currLevel;
    public GameObject nextLevel;

    public GameObject currEndTile;
    public GameObject nextEndTile;

    public TextMeshProUGUI levelCounterObj;
    public int counter = 0;

    public AnimationCurve speedCurve;
    public float duration = 2f;
    public bool IsMultiLevel;

    void Start()
    {
        // Ensure time scale is set to 1
        Time.timeScale = 1;
        if (int.TryParse(levelCounterObj.text, out counter))
        {
            // Successfully converted the text to an integer
            Debug.Log("The level is: " + counter);
        }
        else
        {
            // Handle the case where the text could not be converted
            
            //Debug.LogError("The text in levelCounter is not a valid integer.");
        }
    }


    void Update()
    {
        if (player1IsEnd && player2IsEnd)
        {
            if (!IsMultiLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
            else if (nextEndTile)
            {
                nextEndTile.SetActive(true);
                MovePathsTemp(currLevel, nextLevel);
                //StartCoroutine(TransitionLevels(currLevel, nextLevel));
                counter++;
                levelCounterObj.text = counter.ToString();
                LevelChange.LevelUp(counter);
                currEndTile.SetActive(false);

            }
            else
            {
                Debug.Log("Todos los niveles completados");
            }
            player1IsEnd = false;
            player2IsEnd = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsEnd = true;
            Debug.Log("Player 1 reached the end tile");
        }

        if (other.CompareTag("Player2"))
        {
            player2IsEnd = true;
            Debug.Log("Player 2 reached the end tile");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsEnd = false;
            Debug.Log("Player 1 left the end tile");
        }

        if (other.CompareTag("Player2"))
        {
            player2IsEnd = false;
            Debug.Log("Player 2 left the end tile");
        }
    }

    void MovePathsTemp(GameObject currentLevel, GameObject nextLevel)
    {
        currentLevel.transform.position += new Vector3(0, -1000, 0);
        nextLevel.transform.position += new Vector3(0, -1000, 0);

    }


    //doesnt work rn

    IEnumerator TransitionLevels(GameObject currentLevel, GameObject nextLevel)
    {
        Debug.Log("Transition starting...");
        float startTime = Time.time;
        Vector3 currentLevelStartPos = currentLevel.transform.position;
        Vector3 nextLevelStartPos = nextLevel.transform.position;
        Vector3 currentLevelEndPos = currentLevelStartPos + new Vector3(0, -1000, 0);
        Vector3 nextLevelEndPos = nextLevelStartPos + new Vector3(0, -1000, 0);

        Debug.Log($"Start Time: {startTime}, Duration: {duration}, currentLevelStartPos: {currentLevelStartPos}, nextLevelStartPos: {nextLevelStartPos}, currentLevelEndPos: {currentLevelEndPos}, nextLevelEndPos: {nextLevelEndPos}, Time.time: {Time.time}");

        while (Time.time < startTime + duration)
        {
            float currentTime = Time.time;
            float t = (currentTime - startTime) / duration;
            float curveValue = speedCurve.Evaluate(t);

            Debug.Log($"Current Time: {currentTime}, t: {t}, curveValue: {curveValue}");

            currentLevel.transform.position = Vector3.Lerp(currentLevelStartPos, currentLevelEndPos, curveValue);
            nextLevel.transform.position = Vector3.Lerp(nextLevelStartPos, nextLevelEndPos, curveValue);
            yield return null; // Pause here and continue next frame
        }

        currentLevel.transform.position = currentLevelEndPos;
        nextLevel.transform.position = nextLevelEndPos;
        Debug.Log("Transition finished.");


        /*
        float startTime = Time.time;
        Vector3 currentLevelStartPos = currentLevel.transform.position;
        Vector3 nextLevelStartPos = nextLevel.transform.position;
        Vector3 currentLevelEndPos = currentLevelStartPos + new Vector3(0, -1000, 0);
        Vector3 nextLevelEndPos = nextLevelStartPos + new Vector3(0, -1000, 0);

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float curveValue = speedCurve.Evaluate(t);
            currentLevel.transform.position = Vector3.Lerp(currentLevelStartPos, currentLevelEndPos, curveValue);
            nextLevel.transform.position = Vector3.Lerp(nextLevelStartPos, nextLevelEndPos, curveValue);
            yield return null;
        }

        currentLevel.transform.position = currentLevelEndPos;
        nextLevel.transform.position = nextLevelEndPos;
        */
    }
}
