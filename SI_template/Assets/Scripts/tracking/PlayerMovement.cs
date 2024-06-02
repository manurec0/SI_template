using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    public GameObject endTilesList;
    public TextMeshProUGUI levelCounterObj;
    private int counter;
    public int playerNumber;
    private bool colliderDetect;

    private void Start()
    {
        //initialize the Global counter
        counter = -1;
        levelCounterObj.text = counter.ToString();
        LevelChange.LevelUp(counter);
        colliderDetect = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) DebugEndLevel();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("outPath") && colliderDetect) TransitionLevels();
    }


    public void SetPosition(Vector3 pos)
    {
        //switch playerIndex
        transform.position = pos;
    }

    public void SetRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }


    void TransitionLevels()
    {
        var prevEndTile = endTilesList.transform.GetChild(counter).gameObject;
        var currEndTile = endTilesList.transform.GetChild(counter + 1).gameObject;
        var endTilesTransform = prevEndTile.transform.Find("EndTiles");
        var endTiles = endTilesTransform.gameObject;
        endTiles.GetComponent<MonoBehaviour>().enabled = true;
        
        counter--;
        levelCounterObj.text = counter.ToString();
        currEndTile.SetActive(false);
        prevEndTile.SetActive(true);
        LevelChange.LevelUp(counter); 
        Debug.Log($"Current level: {counter + 1}");

        
        //make the change of levels and is true as we have lost and want to go up
        currEndTile.transform.GetChild(2).position = new Vector3(0, 1000, 0);
        
        if (counter != -1)
        {
            endTiles.SetActive(false);
            var colliders = prevEndTile.transform.Find("colliders").gameObject;
            colliders.SetActive(false);
            var startTiles = prevEndTile.transform.Find("StartTiles").gameObject;
            var boxCollidersStart = startTiles.GetComponents<BoxCollider>();
            foreach (var boxCollider in boxCollidersStart) boxCollider.enabled = true;

            startTiles.GetComponent<MonoBehaviour>().enabled = true;
        }
  
        LevelChange.TriggerMoveObject(true, endTilesTransform);

    }


    void OnEnable()
    {
        LevelChange.OnLevelUp += UpdateLocalCounter;
        LevelChange.OnColliderDetect += SetColliderDetect;
        
    }

    void OnDisable()
    {
        LevelChange.OnLevelUp -= UpdateLocalCounter;
        LevelChange.OnColliderDetect -= SetColliderDetect;

    }

    private void UpdateLocalCounter(int newLevel)
    {
        counter = newLevel;

    }

    private void SetColliderDetect()
    {
        colliderDetect = !colliderDetect;
    }
    private void DebugEndLevel()
    {
        var pos = endTilesList.transform.GetChild(counter +1).GetChild(2).GetChild(playerNumber).GetChild(0);
        SetPosition(pos.position);
    }
   
}
