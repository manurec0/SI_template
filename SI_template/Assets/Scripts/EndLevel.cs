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
    
    public GameObject glowingPlane; // El plano que va a "brillar"
    private Material planeMaterial;
    
    //public bool IsMultiLevel;

    void Start()
    {
        player1IsEnd = false;
        player2IsEnd = false;
        Time.timeScale = 1;
        if (counter == -1) 
        {
            planeMaterial = glowingPlane.GetComponent<Renderer>().material;
            StartCoroutine(GlowEffect());
        }
    }


    void Update()
    {

        //DEBUGGING FUNCTION this will not work well 
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("pressing B");
            debugEndLevel();
        }
        if (player1IsEnd && player2IsEnd)
        {
            if (counter == -1) glowingPlane.SetActive(false);

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

    private IEnumerator GlowEffect()
    {
        Color red = new Color(1, 0, 0, 0.2f); 
        Color black = new Color(0, 0, 0, 0.2f); 
        float duration = 2.0f; 
        
        while (true)
        {
            if (!glowingPlane.activeInHierarchy)
            {
                yield break; 
            }
            yield return LerpColor(planeMaterial.color, red, duration);
            yield return LerpColor(red, black, duration);
        }
    }

    private IEnumerator LerpColor(Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            planeMaterial.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        planeMaterial.color = endColor;
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

    private void debugEndLevel()
    {
        //u should be modifying the position of the players if u want to skip a level
        player1IsEnd = true;
        player2IsEnd = true;
    }
}
