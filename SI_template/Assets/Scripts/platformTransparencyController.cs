using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class platformTransparencyController : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public Material targetTileMaterial; // Material de la tile B
    public float fadeInSpeed = 1f; // Velocidad a la que se incrementará la transparencia
    public float fadeOutSpeed = 1f; // Velocidad a la que se reducirá la transparencia
    private bool isPlayerInZone = false; // Flag para controlar el estado del jugador
     void Start(){
        targetTileMaterial.color = new Color(targetTileMaterial.color.r, targetTileMaterial.color.g, targetTileMaterial.color.b, 0);

     }
    void Update()
    {
            Debug.Log("Position - X: " + player.transform.position.x + ", Z: " + player.transform.position.z);
        // Verificar si el jugador está dentro del área de 1x1 unidades
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 5f &&
            Mathf.Abs(transform.position.z - player.transform.position.z) <= 5f)
        {
            Debug.Log("NOW" + player.transform.position.x + ", Z: " + player.transform.position.z);


            if (!isPlayerInZone)
            {
                isPlayerInZone = true;
                StartCoroutine(FadeIn(targetTileMaterial));
            }
        }
        else
        {
            if (isPlayerInZone)
            {
                isPlayerInZone = false;
                StartCoroutine(FadeOut(targetTileMaterial));
            }
        }
    }

    IEnumerator FadeIn(Material material)
    {
        float alpha = material.color.a;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeInSpeed;
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOut(Material material)
    {
        float alpha = material.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeOutSpeed;
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
            yield return null;
        }
    }
}
