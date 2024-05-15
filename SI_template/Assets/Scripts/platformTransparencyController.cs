using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class platformTransparencyController : MonoBehaviour
{
    // public GameObject player; 
    // public Material targetTileMaterial;
    // public GameObject tile; 
    // public float fadeInSpeed = 1f; 
    // public float fadeOutSpeed = 1f; 
    // private bool isPlayerInZone = false; 
    //  void Start(){
    //     targetTileMaterial.color = new Color(targetTileMaterial.color.r, targetTileMaterial.color.g, targetTileMaterial.color.b, 0);

    //  }
    // void Update()
    // {
    //     MeshCollider meshCollider = tile.GetComponent<MeshCollider>();

    //     if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 5f &&
    //         Mathf.Abs(transform.position.z - player.transform.position.z) <= 5f)
    //     {


    //         if (!isPlayerInZone)
    //         {
    //             isPlayerInZone = true;
    //             StartCoroutine(FadeIn(targetTileMaterial));
    //             meshCollider.enabled = true;


    //         }
    //     }
    //     else
    //     {
    //         if (isPlayerInZone)
    //         {
    //             isPlayerInZone = false;
    //             StartCoroutine(FadeOut(targetTileMaterial));
    //             meshCollider.enabled = false;

    //         }
    //     }
    // }

    // IEnumerator FadeIn(Material material)
    // {
    //     float alpha = material.color.a;
    //     while (alpha < 1)
    //     {
    //         alpha += Time.deltaTime * fadeInSpeed;
    //         material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
    //         yield return null;
    //     }
    // }

    // IEnumerator FadeOut(Material material)
    // {
    //     float alpha = material.color.a;
    //     while (alpha > 0)
    //     {
    //         alpha -= Time.deltaTime * fadeOutSpeed;
    //         material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
    //         yield return null;
    //     }
    // }
}
