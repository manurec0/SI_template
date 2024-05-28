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
    private int counter;
    
    public GameObject glowingPlane; // El plano que va a "brillar"
    private Material planeMaterial;
    
    //public bool IsMultiLevel;

    void Start()
    {
        // Ensure time scale is set to 1
        Time.timeScale = 1;
        int.TryParse(levelCounterObj.text, out counter);
        Debug.Log($"{counter} {currEndTile.name}");
        // Inicializar el material del plano
        if (counter == -1) 
        {
            planeMaterial = glowingPlane.GetComponent<Renderer>().material;
            StartCoroutine(GlowEffect());
        }
    }


    void Update()
    {
        if (player1IsEnd && player2IsEnd)
        {
            if (nextEndTile)
            {
                if (counter == 0) glowingPlane.SetActive(false);
                nextEndTile.SetActive(true);
                Debug.Log($"{counter} {currEndTile.name} potato");
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
        Color red = new Color(1, 0, 0, 0.2f); // Rojo con opacidad 0.2
        Color black = new Color(0, 0, 0, 0.2f); // Negro con opacidad 0.2
        float duration = 2.0f; // Duración de la transición en segundos

        while (true)
        {
            // Asegurarse de que el plano está activo
            if (!glowingPlane.activeInHierarchy)
            {
                yield break; // Terminar la coroutine si el plano está desactivado
            }

            // Interpolar del color actual al rojo
            yield return LerpColor(planeMaterial.color, red, duration);
            // Interpolar del rojo al negro
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
}
