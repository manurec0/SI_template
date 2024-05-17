using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private bool player1IsEnd = false;
    private bool player2IsEnd = false;
    public List<GameObject> levels;
    public AnimationCurve speedCurve;
    public float duration = 2f; /
    private int currentLevelIndex = 0;

    void Update()
    {
        if (player1IsEnd && player2IsEnd)
        {
            if (currentLevelIndex < levels.Count - 1)
            {
                StartCoroutine(TransitionLevels(levels[currentLevelIndex], levels[currentLevelIndex + 1]));
                currentLevelIndex++;
                player1IsEnd = false;
                player2IsEnd = false;
            }
            else
            {
                Debug.Log("Todos los niveles completados");
            }
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

    IEnumerator TransitionLevels(GameObject currentLevel, GameObject nextLevel)
    {
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
    }
}
