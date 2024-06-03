using System.Collections;
using UnityEngine;

public class platformTransparencyController : MonoBehaviour, IPlateAction
{
    public GameObject crackedObject;
    public GameObject hideObject;
    public float transitionTime = 2f;
    public AudioSource transitionSound;

    private BoxCollider boxCollider;
    private IEnumerator currentTransition;

    void Start()
    {
        //SetOpacity(hideObject, 0);
        hideObject.SetActive(false);

        //SetOpacity(crackedObject, 1);
    }

    public void ExecuteAction(bool isActive)
    {
        /*
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }
        currentTransition = FadeTransition(isActive);
        StartCoroutine(currentTransition);
        */
        hideObject.SetActive(isActive);
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = !boxCollider.enabled;

    }

    public void SetOnPause(bool pause)
    {

    }

    private IEnumerator FadeTransition(bool toActive)
    {
        float elapsed = 0;

        hideObject.SetActive(toActive);
        transitionSound.Play();
        StartCoroutine(AudioManager.PitchUp(transitionSound, 1.7f, transitionTime));

        while (elapsed < transitionTime)
        {
            float factor = elapsed / transitionTime;
            SetOpacity(hideObject, toActive ? factor : 1 - factor);
            SetOpacity(crackedObject, toActive ? 1 - factor : factor);
            elapsed += Time.deltaTime;
            yield return null;
        }
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = !boxCollider.enabled;

        SetOpacity(hideObject, toActive ? 1 : 0);
        SetOpacity(crackedObject, toActive ? 0 : 1);
    }

    private void SetOpacity(GameObject obj, float alpha)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
    }
}
