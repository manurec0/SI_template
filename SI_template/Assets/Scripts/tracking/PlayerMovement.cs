using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    public GameObject endTilesList;
    public TextMeshProUGUI levelCounterObj;
    public int counter;

    private void Start()
    {
        //initialize the Global counter
        counter = -1;
        levelCounterObj.text = counter.ToString();
        LevelChange.LevelUp(counter);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("outPath"))
        {
            TransitionLevels();
        }
    }


    public void SetPosition(Vector3 pos)
    {
        //swith playerIndex
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
        counter--;
        levelCounterObj.text = counter.ToString();
        
        currEndTile.SetActive(false);
        prevEndTile.SetActive(true);
        LevelChange.LevelUp(counter); 

        var colliders = prevEndTile.transform.Find("colliders").gameObject;
        colliders.SetActive(false);

        var startTiles = prevEndTile.transform.Find("StartTiles").gameObject;
        var endTilesTransform = prevEndTile.transform.Find("EndTiles");
        var endTiles = endTilesTransform.gameObject;

        endTiles.SetActive(false);

        //var boxCollidersEnd  = endTiles.GetComponents<BoxCollider>();
        //foreach (var boxCollider in boxCollidersEnd) boxCollider.enabled = false;

        var boxCollidersStart = startTiles.GetComponents<BoxCollider>();
        foreach (BoxCollider boxCollider in boxCollidersStart) boxCollider.enabled = true;

        startTiles.GetComponent<MonoBehaviour>().enabled = true;

        //make the change of levels and is true as we have lost and want to go up
        currEndTile.transform.GetChild(2).position = new Vector3(0, 1000, 0);
        LevelChange.TriggerMoveObject(true, endTilesTransform);
    }


    void OnEnable()
    {
        LevelChange.OnLevelUp += UpdateLocalCounter;
    }

    void OnDisable()
    {
        LevelChange.OnLevelUp -= UpdateLocalCounter;
    }

    private void UpdateLocalCounter(int newLevel)
    {
        counter = newLevel;

    }

   
}
