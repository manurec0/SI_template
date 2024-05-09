using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 DebugPos1;
    public Vector3 DebugPos2;
    public TextMeshProUGUI Paco1;
    public TextMeshProUGUI Paco2;
    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    public Quaternion q;
    public bool manual;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -1) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DebugPos1 = new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z);
        DebugPos2 = new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z);
        Paco1.text = DebugPos1.ToString();
        Paco2.text = DebugPos2.ToString();
    }

    public void setPosition(Vector3 pos)
    {
        //swith playerIndex
        transform.position = pos;
    }

    public void setRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }
}
