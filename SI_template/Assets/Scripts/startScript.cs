using System.Collections;
using UnityEngine;

public class startScript : MonoBehaviour
{
    public GameObject glowingPlane; // El plano que va a "brillar"
    private Material planeMaterial;
    private bool player1InTrigger = false;
    private bool player2InTrigger = false;

    void Start()
    {
        // Inicializar el material del plano
        planeMaterial = glowingPlane.GetComponent<Renderer>().material;
        StartCoroutine(GlowEffect());
    }

    void Update()
    {
        // Si ambos jugadores están en el trigger, desactivar el plano
        if (player1InTrigger && player2InTrigger)
        {
            glowingPlane.SetActive(false);
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

    void OnTriggerEnter(Collider other)
    {
        // Marcar cuando cada jugador entra en el trigger
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
        // Marcar cuando cada jugador sale del trigger
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
