using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAgainLevel : MonoBehaviour
{

    private bool player1IsStart;
    private bool player2IsStart;

    public GameObject glowingPlane;
    public GameObject level; //level path to disable colliders of moving and cracked tiles
    
    private Material planeMaterial;
    private GameObject colliders;
    private GameObject endTilesObj;

    private List<GameObject> specialTilesP1;
    private List<GameObject> specialTilesP2;


    void OnEnable()
    {
        glowingPlane.SetActive(true);
        player1IsStart = false;
        player2IsStart = false;
        
        var parentTransform = transform.parent;
        colliders = parentTransform.Find("colliders").gameObject;
        endTilesObj = parentTransform.Find("EndTiles").gameObject;
        endTilesObj.SetActive(false);
        
        specialTilesP1 = GetChildGameObjects(level.transform.GetChild(0).GetChild(0));
        specialTilesP2 = GetChildGameObjects(level.transform.GetChild(1).GetChild(0));
        
        ChangeAllColliders(false);
        
        planeMaterial = glowingPlane.GetComponent<Renderer>().material;

        StartCoroutine(GlowEffect());
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) player1IsStart = true;

        if (other.CompareTag("Player2")) player2IsStart = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) player1IsStart = false;

        if (other.CompareTag("Player2")) player2IsStart = false;
    }

    private void Update()
    {
        //both players are at the start of the level so they can restart the level
        if (player1IsStart && player2IsStart)
        {
            colliders.SetActive(true);
            var boxCollidersEnd = endTilesObj.GetComponents<BoxCollider>();
            foreach (var boxCollider in boxCollidersEnd) boxCollider.enabled = true;
                
            glowingPlane.SetActive(false);
                
            var boxCollidersStart = GetComponents<BoxCollider>();
            foreach (var boxCollider in boxCollidersStart) boxCollider.enabled = false;
            
            ChangeAllColliders(true);
            enabled = false;
        }
    }

    private void OnDisable()
    {
        endTilesObj.SetActive(true);
    }

    private void ChangeAllColliders(bool activate)
    {
        ToggleColliderGeneric(specialTilesP1, activate);
        ToggleColliderGeneric(specialTilesP2, activate);
        LevelChange.ActivateMovingTile(activate);
    }
    
    private void ToggleColliderGeneric(List<GameObject> tiles, bool activate)
    {
        foreach(var obj in tiles)
        {
            var cracked = obj.transform.GetChild(0).GetChild(0);
            cracked.gameObject.GetComponent<BoxCollider>().enabled = activate;
        }
    }
    
    private List<GameObject> GetChildGameObjects(Transform parent)
    {
        var childObjects = new List<GameObject>();
        foreach (Transform child in parent) childObjects.Add(child.gameObject);
        return childObjects;
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
}
