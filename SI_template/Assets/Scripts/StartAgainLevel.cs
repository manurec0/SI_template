using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartAgainLevel : MonoBehaviour
{

    private bool player1IsStart;
    private bool player2IsStart;

    public GameObject canvas;
    public GameObject glowingPlane;
    public GameObject level; //level path to disable colliders of moving and cracked tiles
    
    private Material planeMaterial;
    private int counter;
    private GameObject colliders;
    private GameObject endTilesObj;

    //if for a certain level one of the paths doesnt have a moving or cracked tile the corresponding list will be empty
    private List<GameObject> movingObjs1;
    private List<GameObject> crackedObjs1;
    private List<GameObject> buttonObjs1;
    private List<GameObject> pressureObjs1;

    private List<GameObject> movingObjs2;
    private List<GameObject> crackedObjs2;
    private List<GameObject> buttonObjs2;
    private List<GameObject> pressureObjs2;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("im here");
        glowingPlane.SetActive(true);
        player1IsStart = false;
        player2IsStart = false;

        Transform countTrans = canvas.transform.Find("counter");
        TextMeshProUGUI count = countTrans.GetComponent<TextMeshProUGUI>();
        int.TryParse(count.text, out counter);
        
        canvas.SetActive(false);
        //initialize the gameObjects
        var parentTransform = transform.parent;
        colliders = parentTransform.Find("colliders").gameObject;
        endTilesObj = parentTransform.Find("EndTiles").gameObject;
        if (counter != -1)
        {
            InitializeSpecialTiles(0, out movingObjs1, out crackedObjs1, out buttonObjs1, out pressureObjs1);
            InitializeSpecialTiles(1, out movingObjs2, out crackedObjs2, out buttonObjs2, out pressureObjs2);

            //disable the colliders
            ChangeAllColliders(false);
        }
        planeMaterial = glowingPlane.GetComponent<Renderer>().material;

        StartCoroutine(GlowEffect());


    }

    void InitializeSpecialTiles(int idx, out List<GameObject> move, out List<GameObject> cracked, out List<GameObject> button, out List<GameObject> pressure)
    {
        var playerPath = level.transform.GetChild(idx);
        move = GetChildGameObjects(playerPath.GetChild(0));
        cracked = GetChildGameObjects(playerPath.GetChild(1));
        button = GetChildGameObjects(playerPath.GetChild(2));
        pressure = GetChildGameObjects(playerPath.GetChild(3));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsStart = true;
            Debug.Log("Player 1 reached the start tile");
        }

        if (other.CompareTag("Player2"))
        {
            player2IsStart = true;
            Debug.Log("Player 2 reached the start tile");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsStart = false;
            Debug.Log("Player 1 left the start tile");
        }

        if (other.CompareTag("Player2"))
        {
            player2IsStart = false;
            Debug.Log("Player 2 left the start tile");
        }
    }

    private void LateUpdate()
    {
        //both players are at the start of the level so they can restart the level
        if (player1IsStart && player2IsStart)
        {
            colliders.SetActive(true);
            var boxCollidersEnd = endTilesObj.GetComponents<BoxCollider>();
            foreach (var boxCollider in boxCollidersEnd) boxCollider.enabled = true;
                
            glowingPlane.SetActive(false);
            canvas.SetActive(true);
                
            var boxCollidersStart = GetComponents<BoxCollider>();
            foreach (var boxCollider in boxCollidersStart) boxCollider.enabled = false;
            if(counter != -1)
            {            
                ChangeAllColliders(true);

            }
            enabled = false;

        }

    }

    void ChangeAllColliders(bool activate)
    {
        ToggleColliderGeneric(crackedObjs1, activate);
        ToggleColliderMovingPlat(movingObjs1, activate);
        ToggleColliderGeneric(buttonObjs1, activate);
        ToggleColliderGeneric(pressureObjs1, activate);
        
        ToggleColliderGeneric(crackedObjs2, activate);
        ToggleColliderMovingPlat(movingObjs2, activate);
        ToggleColliderGeneric(buttonObjs2, activate);
        ToggleColliderGeneric(pressureObjs2, activate);
    }
    
    private void ToggleColliderGeneric(List<GameObject> tiles, bool activate)
    {
        foreach(var obj in tiles)
        {
            var cracked = obj.transform.GetChild(0).GetChild(0);
            cracked.gameObject.GetComponent<BoxCollider>().enabled = activate;
        }
    }

    private void ToggleColliderMovingPlat(List<GameObject> movingPlatList, bool activate)
    {
        foreach(var obj in movingPlatList)
        {
            obj.transform.GetChild(0).gameObject.GetComponent<MonoBehaviour>().enabled = activate;
            obj.GetComponent<BoxCollider>().enabled = activate;
        }
    }
    private List<GameObject> GetChildGameObjects(Transform parent)
    {
        List<GameObject> childObjects = new List<GameObject>();

        // Iterate through all children of the parent object
        foreach (Transform child in parent)
        {
            // Add each child GameObject to the list
            childObjects.Add(child.gameObject);
        }

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
