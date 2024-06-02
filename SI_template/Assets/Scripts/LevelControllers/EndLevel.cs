using System.Collections;
using UnityEngine;
using TMPro;


public class EndLevel : MonoBehaviour
{
    private bool player1IsEnd;
    private bool player2IsEnd;

    public GameObject currEndTile;
    public GameObject nextEndTile;

    public TextMeshProUGUI levelCounterObj;
    private int counter = -1;
    
    public GameObject endPos;

    //public bool IsMultiLevel;

    void OnEnable()
    {
        player1IsEnd = false;
        player2IsEnd = false;
        Time.timeScale = 1;

        LevelChange.OnLevelUp += UpdateLocalCounter;

    }


    public void LateUpdate()
    {
        if (player1IsEnd && player2IsEnd)
        {
            if (nextEndTile)
            {
                nextEndTile.SetActive(true);
                counter++;
                levelCounterObj.text = counter.ToString();
                LevelChange.LevelUp(counter);
                Debug.Log($"Current level: {counter + 1}");
                currEndTile.transform.GetChild(2).position = new Vector3(0, -1000, 0);
                currEndTile.SetActive(false);
                LevelChange.TriggerMoveObject(false, nextEndTile.transform.GetChild(2));
            }
            else
            {
                currEndTile.SetActive(false);
                endPos.SetActive(true);
                LevelChange.TriggerMoveObject(false, currEndTile.transform.GetChild(2));
            }
            player1IsEnd = false;
            player2IsEnd = false;
            enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsEnd = true;
        }

        if (other.CompareTag("Player2"))
        {
            player2IsEnd = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsEnd = false;
        }

        if (other.CompareTag("Player2"))
        {
            player2IsEnd = false;
        }
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
