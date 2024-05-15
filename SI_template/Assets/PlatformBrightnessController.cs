using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrightnessController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Color targetColor; // Color objetivo configurable desde el inspector
    public float transitionDuration = 1f; // Duración de la transición
    private float transitionTime = 0f;
    private bool isChanging = false; // Controla si la transición está activa
    private Color originalColor;

    void Start()
    {
        Debug.Log(targetColor);
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if ( !isChanging)
        {
            isChanging = true;
            transitionTime = 0f;
            StartCoroutine(ChangeColor(targetColor)); // Inicia la transición hacia el color objetivo
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ( isChanging)
        {
            isChanging = false;
            transitionTime = 0f;
            StartCoroutine(ChangeColor(originalColor)); // Inicia la transición de regreso al color original
        }
    }

    IEnumerator ChangeColor(Color newColor)
    {
        Debug.Log(newColor);
        while (transitionTime < transitionDuration)
        {
            transitionTime += Time.deltaTime;
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, newColor, transitionTime / transitionDuration);
            yield return null;
        }
        meshRenderer.material.color = newColor; // Asegura que el color final es exactamente el esperado
    }
}
