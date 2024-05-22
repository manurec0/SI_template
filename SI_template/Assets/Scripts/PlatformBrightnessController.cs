using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrightnessController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Color targetColor;
    public float transitionDuration = 1f;
    private float transitionTime = 0f;
    private bool isChanging = false;
    private Color originalColor;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            if (!isChanging)
            {
                isChanging = true;
                transitionTime = 0f;
                StartCoroutine(ChangeColor(targetColor));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            if (isChanging)
            {
                isChanging = false;
                transitionTime = 0f;
                StartCoroutine(ChangeColor(originalColor));
            }
        }
    }

    IEnumerator ChangeColor(Color newColor)
    {
        while (transitionTime < transitionDuration)
        {
            transitionTime += Time.deltaTime;
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, newColor, transitionTime / transitionDuration);
            yield return null;
        }
        meshRenderer.material.color = newColor;
    }
}
