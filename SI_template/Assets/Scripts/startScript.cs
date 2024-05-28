using System.Collections;
using UnityEngine;

public class startScript : MonoBehaviour
{
    public GameObject glowingPlane;
    private Material planeMaterial;
    private bool player1InTrigger = false;
    private bool player2InTrigger = false;

    void Start()
    {
        planeMaterial = glowingPlane.GetComponent<Renderer>().material;
        StartCoroutine(GlowEffect());
    }

    void Update()
    {
        if (player1InTrigger && player2InTrigger)
        {
            glowingPlane.SetActive(false);
        }
    }

    private IEnumerator GlowEffect()
    {
        if (player1InTrigger && player2InTrigger)
        {
            glowingPlane.SetActive(false);
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player 1 has entered the trigger.");
            player1InTrigger = true;
        }
        else if (other.CompareTag("Player2"))
        {
            Debug.Log("Player 2 has entered the trigger.");
            player2InTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1InTrigger = false;
        }
        else if (other.CompareTag("Player2"))
        {
            player2InTrigger = false;
        }
    }
}
