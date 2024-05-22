using UnityEngine;
using TMPro;


public class PlayerPosition : MonoBehaviour
{
    private Vector3 DebugPos1;
    private Vector3 DebugPos2;
    public TextMeshProUGUI Paco1;
    public TextMeshProUGUI Paco2;
    public TextMeshProUGUI levelMsg;
    public TextMeshProUGUI levelCounterObj;

    public GameObject player1;
    public GameObject player2;

    void Update()
    {
        
        DebugPos1 = new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z);
        DebugPos2 = new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z);
        Paco1.text = DebugPos1.ToString();
        Paco2.text = DebugPos2.ToString();
        int.TryParse(levelCounterObj.text, out int counter);

        levelMsg.text = "Level: " + (counter + 1).ToString();

    }
}
