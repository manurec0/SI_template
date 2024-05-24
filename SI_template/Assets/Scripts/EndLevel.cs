using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class EndLevel : MonoBehaviour
{
    private bool player1IsEnd = false;
    private bool player2IsEnd = false;

    public GameObject currEndTile;
    public GameObject nextEndTile;

    public TextMeshProUGUI levelCounterObj;
    private int counter = 0;

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
                counter++;
                levelCounterObj.text = counter.ToString();
                LevelChange.LevelUp(counter);
                currEndTile.transform.GetChild(2).position = new Vector3(0, -1000, 0);
                currEndTile.SetActive(false);

                LevelChange.TriggerMoveObject(false, nextEndTile.transform.GetChild(2));

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
