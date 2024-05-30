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


    void Update()
    {
        if (player1IsEnd && player2IsEnd)
        {
            if (nextEndTile)
            {
                nextEndTile.SetActive(true);
                counter++;
                levelCounterObj.text = counter.ToString();
                LevelChange.LevelUp(counter);
                currEndTile.transform.GetChild(2).position = new Vector3(0, -1000, 0);
                currEndTile.SetActive(false);
                LevelChange.TriggerMoveObject(false, nextEndTile.transform.GetChild(2));


            }
            else
            {
                currEndTile.SetActive(false);
                endPos.SetActive(true);
                LevelChange.TriggerMoveObject(false, currEndTile.transform.GetChild(2));


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

    void OnDisable()
    {
        LevelChange.OnLevelUp -= UpdateLocalCounter;
    }

    private void UpdateLocalCounter(int newLevel)
    {
        counter = newLevel;

    }
}
